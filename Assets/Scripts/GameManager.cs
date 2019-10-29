using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool Running = false;
    public float Speed = 0f;
    private System.Random random = new System.Random();
    public GameObject[] Spawnables;
    public TextMeshProUGUI txtScore;
    public int Score = 0;
    public TextMeshProUGUI txtHighscore;
    private float nextspawn;
    public delegate void resetCallback();
    public static resetCallback OnReset;
    public static GameObject DebugPanel;
    public Light Sun;
    private static GameObject startTxt;
    private static float newGameWait;
    public static GameObject GameOverCanvas;
    public static GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData = new GameData();
        GameData.Load();

        nextspawn = (float)random.NextDouble()*2;
        DebugPanel = FindObjectOfType<DebugPanel>().gameObject;
        DebugPanel.SetActive(false);
        GameOverCanvas = GameObject.Find("GUI/GameOverCanvas");
        GameOverCanvas.SetActive(false);
        startTxt = GameObject.Find("GUI/IngameCanvas/Start");
        txtHighscore.text = Highscore.GetTop().Score.ToString();
    }

    public void NewGame(){
        if(Time.realtimeSinceStartup>=newGameWait){
            Debug.Log("New game started.");
            Time.timeScale = 1f;
            Running = true;
            Speed = 7;
            Score = 0;

            txtHighscore.text = Highscore.GetTop().Score.ToString();

            startTxt.SetActive(false);
            GameOverCanvas.SetActive(false);
            OnReset();
        }else Debug.Log("Need to wait for "+Math.Abs(Time.realtimeSinceStartup-newGameWait).ToString()+" more seconds.");
    }
    public void GameOver(){
        Time.timeScale = 0f;
        Running = false;
        Speed = 0;
        newGameWait = Time.realtimeSinceStartup + 1f;
        GameOverCanvas.SetActive(true);
        GameObject.Find("GUI/GameOverCanvas/NewHighscore").SetActive(Score>Highscore.GetBot().Score);
        PrintLeaderboard();
    }
    public void PrintLeaderboard(){
        gameData.Highscores.Sort();
        TextMeshProUGUI Leaderboard = GameObject.Find("GUI/GameOverCanvas/Leaderboard/Viewport/Content/txtLeaderboard").GetComponent<TextMeshProUGUI>();
        Leaderboard.text = "";
        for(var i=0;i<gameData.Highscores.Count;i++){
            Leaderboard.text += "<line-height=0.001em><align=left>"+(i+1).ToString()+") "+gameData.Highscores[i].Username+"\n<align=right>"+gameData.Highscores[i].Score.ToString()+"<line-height=1em>\n";
        }
    }
    public void SaveHighscore(){
        TextMeshProUGUI Username = GameObject.Find("GUI/GameOverCanvas/NewHighscore/Editor/Username/Text Area/Text").GetComponent<TextMeshProUGUI>();
        new Highscore(Username.text,Score);
        IgnoreHighscore();
        PrintLeaderboard();
        GameData.Save();
    }
    public void IgnoreHighscore(){
        GameObject.Find("GUI/GameOverCanvas/NewHighscore").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Opening and closing the debug panel is managed here
        if(Input.GetKeyDown("`")){
            DebugPanel.SetActive(!DebugPanel.activeSelf);
        }
        if(Running){
            // Update the score
            Score = (int)((Speed-7)*100);
            txtScore.text = Score.ToString();
            if(Score>Highscore.GetTop().Score){
                txtHighscore.text = Score.ToString();
            }
            // Day/Night cycle
            if(Score>250)
                Sun.intensity = Math.Max(0f,Math.Min(1f,1.5f*(float)Math.Sin(Math.PI/5*Score/100)+0.5f));
            else
                Sun.intensity = 1;

            // Update the speed
            Speed+=Time.deltaTime/10;
            
            // Spawn obstacles
            nextspawn-=Time.deltaTime;
            if(nextspawn<=0f){
                var target = Spawnables[random.Next(0,(Score<500?2:Spawnables.Length))];
                if(target.transform.localPosition.x<-20f) target.transform.localPosition = new Vector3(20f,target.transform.localPosition.y,target.transform.localPosition.z);

                nextspawn = (float)Math.Max(random.NextDouble()*2,0.5);
            }
        }
    }
}

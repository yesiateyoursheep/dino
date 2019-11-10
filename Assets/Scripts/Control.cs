using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Control : MonoBehaviour
{
    private float timeOfTravel;
    private float currentTime;
    public GameManager gameManager;

    
    public Dino dino;
	private float swipe = 0f;
	private float swipestart = 0f;
	private float swipeend = 0f;
    public void Login(){
        GameManager.gameData.Login(
              GameObject.Find("GUI/LoginCanvas/Login/Form/Username").GetComponent<TMP_InputField>().text,
              GameObject.Find("GUI/LoginCanvas/Login/Form/Password").GetComponent<TMP_InputField>().text
        );
        //Event.current.Use();
    }
    public void Register(){
        GameManager.gameData.Register(
              GameObject.Find("GUI/LoginCanvas/Login/Form/Username").GetComponent<TMP_InputField>().text,
              GameObject.Find("GUI/LoginCanvas/Login/Form/Password").GetComponent<TMP_InputField>().text
        );
        //Event.current.Use();
    }
    public void Quit(){
        Application.Quit();
        //Event.current.Use();
    }
    public void Pause(){
        gameManager.Pause();
    }
    public void ToggleServerPanel(){
        TextMeshProUGUI btnToggle = GameObject.Find("GUI/LobbyCanvas/ServerSelect/Toggle/Text (TMP)").GetComponent<TextMeshProUGUI>();
        RectTransform LobbyCanvas = GameObject.Find("GUI/LobbyCanvas/ServerSelect").GetComponent<RectTransform>();

        btnToggle.text = (btnToggle.text=="<")?">":"<";

        timeOfTravel = 0.25f;
        currentTime = 0f;

        StartCoroutine(Animate((btnToggle.text=="<")?new Vector3(-200f,0f,0f):new Vector3(0f,0f,0f),(btnToggle.text=="<")?new Vector3(0f,0f,0f):new Vector3(-200f,0f,0f),LobbyCanvas));
    }
    public void Jump(){
        if(gameManager.Running) dino.up = true;
        else gameManager.NewGame();
    }
    public void Crouch(){
        if(gameManager.dinofocus) dino.dn = true;
    }
    IEnumerator Animate(Vector3 start, Vector3 end, RectTransform target){
        while(currentTime<timeOfTravel){
            currentTime+=Time.fixedDeltaTime;
            target.anchoredPosition = Vector3.Lerp(start,end,currentTime/timeOfTravel);
            yield return null;
        }
    }
    // Handle input reliably
	void OnGUI(){
		// Only detect input when the debugpanel and gameovercanvas are inactive.
		if(gameManager.dinofocus){
			if(gameManager.Running){
                if(Input.GetKey(KeyCode.Escape)) gameManager.Pause();
				// Jump or crouch when the game is running
				dino.up = (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W));
				if(!dino.up) dino.dn = (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S));
			}else{
				// Start the game if it isn't running and the player jumps.
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.S)){
					gameManager.NewGame();
				}
			}
		}
	}

	void FixedUpdate()
	{
		// Handling swipes needs to be done in FixedUpdate()
		// This method handles touch input differently from mouse, keyboard and controller.
		if(gameManager.Running){
			if(Input.touchCount > 0){
				// If there is any touch input, disregard mouse, keyboard and controller.
				dino.up = false;
				dino.dn = false;
				// Track the vertical trajectory of any swipe
				if(swipe==0f){
					swipestart = Input.GetTouch(0).position.y;
					swipe = 0f;
				}
				else{
					swipeend = Input.GetTouch(0).position.y;
				}
				swipe += Time.deltaTime;
			}else if(swipe>0f){
				// Crouch if the user swiped down
				if(swipestart - swipeend > 50f){
					dino.dn = true;
				}
				// Otherwise jump
				else dino.up = true;
				swipe -= (dino.dn?Time.deltaTime/2:Time.deltaTime);
			}
			else{
				swipe = 0f;
			}
		}
    }
}

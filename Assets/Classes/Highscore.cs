using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Highscore : IComparable<Highscore>
{
    public string Username;
    public int Score;

    public Highscore(string pUsername = "undefined",int pScore = 0)
    {
        if(pUsername.Length>3){
            Username = pUsername;
            Score = pScore;
            GameManager.gameData.Highscores.Add(this);
            while(GameManager.gameData.Highscores.Count>10){
                GameManager.gameData.Highscores.Remove(GetBot());
            }
        }
    }
    
    public static Highscore GetTop(){
        var bot = 0;
        Highscore tophi = null;
        for(var i = 0; i < GameManager.gameData.Highscores.Count; i++){
            if(GameManager.gameData.Highscores[i].Score>bot){
                bot = GameManager.gameData.Highscores[i].Score;
                tophi = GameManager.gameData.Highscores[i];
            }
        }
        if(tophi==null) return new Highscore();
        return tophi;
    }
    public static Highscore GetBot(){
        var bot = 9999;
        Highscore bothi = null;
        for(var i = 0; i < GameManager.gameData.Highscores.Count; i++){
            if(GameManager.gameData.Highscores[i].Score<bot){
                bot = GameManager.gameData.Highscores[i].Score;
                bothi = GameManager.gameData.Highscores[i];
            }
        }
        if(bothi==null) return new Highscore();
        return bothi;
    }
    int IComparable<Highscore>.CompareTo(Highscore hs2){
        if(Score==hs2.Score) return 0;
        return Score>hs2.Score?-1:1;
    }
}
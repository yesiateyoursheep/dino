using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Highscore
{
    //[PrimaryKey]
    public int Id;
    private static int autoincrement = 0;
    public string Username;
    public int Score;

    public Highscore(){
        
    }

    public static void New(string pUsername = "",int pScore = 0)
    {
        if(pUsername.Length>3){ // Usernames must have a substantial length
            Highscore hs = new Highscore{
                Id = autoincrement++,
                Username = pUsername,
                Score = pScore
            };
            //GameManager.gameData.Dataservice.InsertIfDoesNotExist<Highscore>(hs);
        }
    }
    
    public static Highscore GetTop(){
        //Highscore tophi = (Highscore)GameManager.gameData.Dataservice.Connection.Query<Highscore>("SELECT *").OrderByDescending(i=>i.Score).Take(1);
        Highscore tophi = null;
        if(tophi==null) return new Highscore();
        return tophi;
    }
    public static Highscore GetBot(){
        //Highscore bothi = (Highscore)GameManager.gameData.Dataservice.Connection.Query<Highscore>("SELECT *").OrderBy(i=>i.Score).Take(1);
        Highscore bothi = null;
        if(bothi==null) return new Highscore();
        return bothi;
    }
}
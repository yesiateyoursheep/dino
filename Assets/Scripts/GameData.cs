using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SQLite4Unity3d;

public class GameData
{
    public DataService Dataservice = new DataService("game.db");
    public List<Highscore> Highscores = new List<Highscore>();
    public GameData(){
        
    }
    public bool Init(){
        Dataservice.CreateDB(new []{typeof(Highscore)});

        string[] defaultname = {"Clyde","Freddy","Anderson","Clayton","Cleetus","Big Red","Roderich","Phil","Collin","Jacky"};
        int[] defaultscore = {50,40,35,33,29,18,16,10,7,4};
        while(GameManager.gameData.Highscores.Count<10){
            Highscore.New(defaultname[GameManager.gameData.Highscores.Count],defaultscore[GameManager.gameData.Highscores.Count]);
        }
        return true;
    }
}

/*
create table Highscore(
    id integer primary key autoincrement,
    name text not null,
    score int not null
);

insert into highscore(name,score) values("Clyde",50);
insert into highscore(name,score) values("Freddy",40);
insert into highscore(name,score) values("Anderson",35);
insert into highscore(name,score) values("Clayton",33);
insert into highscore(name,score) values("Cleetus",29);
insert into highscore(name,score) values("Big Red",18);
insert into highscore(name,score) values("Roderich",16);
insert into highscore(name,score) values("Phil",10);
insert into highscore(name,score) values("Collin",7);
insert into highscore(name,score) values("Jacky",4);

 */
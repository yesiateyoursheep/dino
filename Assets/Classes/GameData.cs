using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SQLite.net;
using SQLite4Unity3d;

[Serializable]
public class GameData
{
    public List<Highscore> Highscores = new List<Highscore>();
    public GameData(){
        
    }
    public static void Save(){
        if(!Directory.Exists(Path.Combine(Application.persistentDataPath,"Save")))
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath,"Save"));
        
        if(File.Exists(Path.Combine(Application.persistentDataPath, "Save/game.db")))
            File.Copy(Path.Combine(Application.persistentDataPath, "Save/game.db"),Path.Combine(Application.persistentDataPath, "Save/game.db.bak"),true);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream save = File.Create(Path.Combine(Application.persistentDataPath, "Save/game.db"));

        formatter.Serialize(save,GameManager.gameData);

        save.Close();
    }
    public static bool Load(){
        if(File.Exists(Path.Combine(Application.persistentDataPath, "Save/game.db"))){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream save = File.Open(Path.Combine(Application.persistentDataPath, "Save/game.db"),FileMode.Open);

            GameManager.gameData = (GameData)formatter.Deserialize(save);

            save.Close();
        }

        string[] defaultname = {"Clyde","Freddy","Anderson","Clayton","Cleetus","Big Red","Roderich","Phil","Collin","Jacky"};
        int[] defaultscore = {50,40,35,33,29,18,16,10,7,4};
        while(GameManager.gameData.Highscores.Count<10){
            new Highscore(defaultname[GameManager.gameData.Highscores.Count],defaultscore[GameManager.gameData.Highscores.Count]);
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
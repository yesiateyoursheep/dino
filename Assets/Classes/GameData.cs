using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GameData
{
    public List<Highscore> Highscores = new List<Highscore>();
    public GameData(){
        
    }
    public static void Save(){
        if(!Directory.Exists(Path.Combine(Application.persistentDataPath,"Save")))
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath,"Save"));
        
        if(File.Exists(Path.Combine(Application.persistentDataPath, "Save/01.bin")))
            File.Copy(Path.Combine(Application.persistentDataPath, "Save/01.bin"),Path.Combine(Application.persistentDataPath, "Save/01.bak"),true);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream save = File.Create(Path.Combine(Application.persistentDataPath, "Save/01.bin"));

        formatter.Serialize(save,GameManager.gameData);

        save.Close();
    }
    public static bool Load(){
        if(File.Exists(Path.Combine(Application.persistentDataPath, "Save/01.bin"))){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream save = File.Open(Path.Combine(Application.persistentDataPath, "Save/01.bin"),FileMode.Open);

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

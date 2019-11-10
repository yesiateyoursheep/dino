using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Control : MonoBehaviour
{
    private GameManager gameManager;
    public void Login(){
        GameManager.gameData.Login(
              GameObject.Find("GUI/LoginCanvas/Login/Form/Username").GetComponent<TMP_InputField>().text,
              GameObject.Find("GUI/LoginCanvas/Login/Form/Password").GetComponent<TMP_InputField>().text
        );
    }
    public void Register(){
        GameManager.gameData.Register(
              GameObject.Find("GUI/LoginCanvas/Login/Form/Username").GetComponent<TMP_InputField>().text,
              GameObject.Find("GUI/LoginCanvas/Login/Form/Password").GetComponent<TMP_InputField>().text
        );
    }
}

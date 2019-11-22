using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Simple controller object for the debug panel

public class Chat : MonoBehaviour
{
    public GameObject preview;
    public TMP_InputField InputText;
    public TextMeshProUGUI OutputText;
    public TextMeshProUGUI ChatPreviewText;
    private bool enter;
    void Start()
    {
        ChatPreviewText = preview.GetComponentInChildren<TextMeshProUGUI>(true);
    }

    void Update()
    {
        if(enter&&InputText.text.Length>0){
            GameManager.gameData.SendAMessage(InputText.text);
            InputText.text="";
        }
        enter = false;
    }
    // Input is handled in OnGUI(), while actions are taken on Update()
    void OnGUI(){
        if(Input.GetKeyDown(KeyCode.Return)){
            enter = true;
        }
    }
    public void btnSend_Click(){
        enter = true;
    }

    public void AddLine(Message message){
        OutputText.text+=message.author.username+"#"+message.author.id+"> "+message.content+"\n";
        ChatPreviewText.text = message.author.username+"#"+message.author.id+"> "+message.content+"\n";
    }
    public void AddLine(string line){
        OutputText.text+=line+"\n";
        ChatPreviewText.text = OutputText.text;
    }
}

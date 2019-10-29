using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Simple controller object for the debug panel

public class DebugPanel : MonoBehaviour
{
    public GameObject input;
    public GameObject output;
    TextMeshProUGUI line;
    TextMeshProUGUI text;
    private bool enter;
    void Start()
    {
        text = output.GetComponent<TextMeshProUGUI>();
        line = input.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(enter){
            AddLine();
        }
        enter = false;
    }
    // Input is handled in OnGUI(), while actions are taken on Update()
    void OnGUI(){
        if(Input.GetKeyDown(KeyCode.Return)){
            enter = true;
        }
    }

    public void AddLine(){
        text.text+="> "+line.text+"\n";
        line.text="";
    }
}

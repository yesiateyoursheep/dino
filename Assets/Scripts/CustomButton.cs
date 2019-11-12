using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomButton : MonoBehaviour
{
    public Control control;
    public enum act{Jump,Crouch};
    public act action;
    public bool up = false;
    public bool dn = false;
    public void Click(){
        if(control.gameManager.dinofocus){
            switch(action){
                case act.Jump:
                    if(!control.gameManager.Running) control.gameManager.NewGame();
                    up = true;
                    break;
                case act.Crouch:
                    if(control.gameManager.Running) dn = true;
                    break;
                default:
                    Debug.Log("Unhandled action!");
                    break;
            }
        }
    }
    public void UnClick(){
        switch(action){
            case act.Jump:
                up = false;
                break;
            case act.Crouch:
                dn = false;
                break;
            default:
                Debug.Log("Unhandled action!");
                break;
        }
    }
    void Update(){
        if(up) control.dino.up = true;
        if(dn) control.dino.dn = true;
    }
}

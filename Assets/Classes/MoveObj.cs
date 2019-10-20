using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    public bool Loop = true;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        GameManager.OnReset += OnReset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Time.deltaTime * GameManager.Speed, 0f, 0f);
        if (transform.position.x <= -17f & Loop)
        {
            transform.Translate(31f, 0f, 0f);
        }
    }
    void OnReset(){
        transform.position = startPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ptero : MonoBehaviour
{
	public GameObject avatar;
	private GameManager gameManager;
	public List<Mesh> Anim;
	private int _AnimNum = 0;
	private float _AnimWait = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.Running & transform.position.x >= -17.0f){
			_AnimWait -= Time.deltaTime;
			if (_AnimWait < 0)
			{
				_AnimWait += 1.25f/gameManager.Speed;
				_AnimNum = _AnimNum == 1 ? 0 : 1;
				avatar.GetComponent<MeshFilter>().mesh = Anim[_AnimNum];
			}
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
	public Rigidbody rb;
	private BoxCollider hitbox;
	public GameObject avatar;
	private GameManager gameManager;
	public List<Mesh> Anim;
	private int _AnimNum = 0;
	private int _AnimOffset;
	private bool _AnimRun;
	private float _AnimWait = 0.25f;
	private Vector3 _StartPos;
	private bool up;
	private bool dn;
	private Vector3 starthitcenter;
	private Vector3 starthitsize;
	private float swipe = 0f;
	private float swipestart = 0f;
	private float swipeend = 0f;

	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		hitbox = gameObject.GetComponent<BoxCollider>();
		_StartPos = gameObject.transform.position;
		GameManager.OnReset += OnReset;
		starthitcenter = hitbox.center;
		starthitsize = hitbox.size;
	}

	void OnReset(){
		gameObject.transform.position = _StartPos;
	}

	// Handle input reliably
	void OnGUI(){
		if(!GameManager.DebugPanel.activeSelf&&!GameManager.GameOverCanvas.activeSelf){
			if(gameManager.Running){
				up = (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(0));
				if(!up) dn = (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S));
			}else{
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.touchCount > 0 || Input.GetMouseButtonDown(0)){
					gameManager.NewGame();
				}
			}
		}
	}

	void FixedUpdate()
	{
		if(gameManager.Running){
			if(Input.touchCount > 0){
				up = false;
				if(swipe==0f){
					swipestart = Input.GetTouch(0).position.y;
					swipe = 0f;
				}
				else{
					swipeend = Input.GetTouch(0).position.y;
				}
				swipe += Time.deltaTime;
			}else if(swipe>0f){
				//Debug.Log((swipestart - swipeend).ToString());
				if(swipestart - swipeend > 50f){
					dn = true;
				}
				else up = true;
				swipe -= (dn?Time.deltaTime/2:Time.deltaTime);
			}
			else{
				swipe = 0f;
			}
		}

		_AnimOffset = dn?2:0;

		// Handle input
		if (this.transform.position[1] < 0.2)
		{
			_AnimRun = true;
			if (up)
			{
				rb.velocity = new Vector3(0, 7, 0);
			}
		}
		else if (this.transform.position[1] > 0.2)
		{
			_AnimRun = false;
			if (dn)
			{
				rb.velocity = new Vector3(0, -10, 0);
			}
			else if (up){
				rb.AddForce(new Vector3(0, 5, 0));
			}
		}

		// Change animation
		if(gameManager.Running){
			_AnimWait -= Time.deltaTime;
			if (_AnimWait < 0)
			{
				_AnimWait += 1.25f/gameManager.Speed;
				if (_AnimRun) _AnimNum = _AnimNum == 1 ? 0 : 1;
			}
			avatar.GetComponent<MeshFilter>().mesh = Anim[_AnimNum + _AnimOffset];
			if(_AnimOffset==2){
				hitbox.center = new Vector3(0.4f,0.5f,0f);
				hitbox.size = new Vector3(1f,1f,0.1f);
			}else{
				hitbox.center = starthitcenter;
				hitbox.size = starthitsize;
			}
		}else avatar.GetComponent<MeshFilter>().mesh = Anim[4];
		up = false;
		dn = false;
	}

	// Collision handling
	void OnTriggerEnter(Collider other)
	{
		gameManager.GameOver();
		avatar.GetComponent<MeshFilter>().mesh = Anim[5];
	}
}

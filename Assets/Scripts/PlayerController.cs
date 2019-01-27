using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour{

	public float speed;
	public string axis;
	public float upLimit;
	public float downLimit;
	
	
	private void Awake()
	{
		if(transform.position.x > 0) transform.GetComponent<SpriteRenderer>().color = Color.black;
		else transform.GetComponent<SpriteRenderer>().color = Color.red;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer)
			return;
		float move = Input.GetAxis(axis)* speed * Time.deltaTime;
		float nextPos = transform.position.y + move;
		if(nextPos > upLimit)
		{
			move = 0;
		}
		if(nextPos < downLimit)
		{
			move = 0;
		}
		transform.Translate(move, 0, 0);
	}
	float GetInputPC()
	{
		return Input.GetAxis(axis) * speed * Time.deltaTime;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallManage : NetworkBehaviour{

	public GameObject prefabBola;
	bool bolaMuncul = false;
	GameObject ball;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!isServer || bolaMuncul)
			return;
		if(NetworkServer.connections.Count == 2)
		{
			ball = (GameObject)Instantiate(prefabBola);
			NetworkServer.Spawn(ball);
			bolaMuncul = true;
		}
	}
}

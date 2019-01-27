using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BallControll : NetworkBehaviour{

	public int tekanan;
	Rigidbody2D rigid;
	[SyncVar(hook = "OnChangeScore1")]
	public int score1;
	[SyncVar(hook = "OnChangeScore2")]
	public int score2;
	Text scoreUI1;
	Text scoreUI2;
	GameObject endPanel;
	Text txWinner;
	AudioSource audio;
	public AudioClip hitSound;
	// Use this for initialization
	void Start () {
		endPanel = GameObject.Find("EndPanel");
		endPanel.SetActive(false);
		score1 = 0;
		score2 = 0;
		scoreUI1 = GameObject.Find("P1Score").GetComponent<Text>();
		scoreUI2 = GameObject.Find("P2Score").GetComponent<Text>();
		rigid = GetComponent<Rigidbody2D>();
		Vector2 arah = new Vector2(5,0).normalized;
		rigid.AddForce(arah * tekanan);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (!isServer)
			return;
		if(coll.gameObject.name.Contains("Player1"))
		{
			float sudut = (transform.position.y - coll.transform.position.y) * 5f;
			Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
			rigid.velocity = new Vector2(0, 0);
			rigid.AddForce(arah * tekanan * 2);
		}
		
		else if(coll.gameObject.name == "Gawang2")
		{
			score1 += 1;
			if(score1 == 5)
			{
				RpcTampilanSelesai("Black");
				return;
			}
			ResetBall();
			Vector2 arah = new Vector2(2, 0).normalized;
			rigid.AddForce(arah * tekanan);
		}
		else if(coll.gameObject.name == "Gawang1")
		{
			score2 += 1;
			if(score2 == 5)
			{
				RpcTampilanSelesai("Red");
				return;
			}
			ResetBall();
			Vector2 arah = new Vector2(-2, 0).normalized;
			rigid.AddForce(arah * tekanan);
		}
		if(coll.gameObject.name == "Player1" || coll.gameObject.name == "Player2")
		{
			float sudut = (transform.position.y - coll.transform.position.y) * 5f;
			Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
			rigid.velocity = new Vector2(0, 0);
			rigid.AddForce(arah * tekanan * 2);
		}
		if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
		{
			ClientDisconnect();
		}
		audio.PlayOneShot(hitSound);
	}
	[ClientRpc]
	void RpcTampilanSelesai(string warna)
	{
		endPanel.transform.localPosition = Vector3.zero;
		txWinner = GameObject.Find("Winner").GetComponent<Text>();
		txWinner.text = "Player " + warna + " Winner!";
		gameObject.SetActive(false);
	}
	
	void ResetBall()
	{
		transform.localPosition = new Vector2(0, 0);
		rigid.velocity = new Vector2(0, 0);
	}
	
	void OnChangeScore1(int score)
	{
		if(scoreUI1 != null)
			scoreUI1.GetComponent<Text>().text = "" + score;
	}
	
	void OnChangeScore2(int score)
	{
		if(scoreUI2 != null)
			scoreUI2.GetComponent<Text>().text = "" + score;
	}
	
	void ClientDisconnect()
	{
		GameObject.Find("Game Manager").SendMessage("BackToMain");
	}
}
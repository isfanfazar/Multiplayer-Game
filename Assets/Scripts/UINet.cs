using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UINet : MonoBehaviour {

	GameObject multiplayerPanel;
	Button btnHost;
	Button btnJoin;
	Button btnCancel;
	Text txInfo;
	NetworkManager network;
	int status = 0;
	// Use this for initialization
	void Start () {
		multiplayerPanel = GameObject.Find("MultiplayerPanel");
		multiplayerPanel.transform.localPosition = Vector3.zero;
		btnHost = GameObject.Find("BtnHost").GetComponent<Button>();
		btnJoin = GameObject.Find("BtnJoin").GetComponent<Button>();
		btnCancel = GameObject.Find("BtnCancel").GetComponent<Button>();
		txInfo = GameObject.Find("Info").GetComponent<Text>();
		btnHost.onClick.AddListener(StartHostGame);
		btnJoin.onClick.AddListener(StartJoinGame);
		btnCancel.onClick.AddListener(CancelConnection);
		btnCancel.interactable = false;
		network = GameObject.Find("GameManager").GetComponent<NetworkManager>();
		txInfo.text = "Info: Server Address " + network.networkAddress + " with port " + network.networkPort;
		string ip = Network.player.ipAddress;
		Debug.Log(ip);
	}
	
	// Update is called once per frame
	void Update () {
		if(NetworkClient.active || NetworkServer.active)
		{
			btnHost.interactable = false;
			btnJoin.interactable = false;
			btnCancel.interactable = true;
		}
		else
		{
			btnHost.interactable = true;
			btnJoin.interactable = true;
			btnCancel.interactable = false;
		}
		if(NetworkServer.connections.Count == 2 && status == 0)
		{
			status = 1;
			StartGame();
		}
		if(ClientScene.ready && !NetworkServer.active && status == 0)
		{
			status = 1;
			StartGame();
		}
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			BackToMenu();
		}
		
	}
	
	private void StartHostGame()
	{
		Debug.Log("Host Button Clicked");
		if(!NetworkServer.active)
		{
			network.StartHost();
		}
		if(NetworkServer.active)
			txInfo.text = "Info: Waiting other Player(If Server Is Active)";
	}
	
	private void StartJoinGame()
	{
		Debug.Log("Join Button Clicked");
		if(!NetworkClient.active)
		{
			network.StartClient();
			network.client.RegisterHandler(MsgType.Disconnect, ConnectionError);
		}
		if(NetworkClient.active)
			txInfo.text = "Info: Try Connect To Server";
	}
	
	private void CancelConnection()
	{
		Debug.Log("Cancel Button Clicked");
		network.StopHost();
		btnHost.interactable = true;
		btnJoin.interactable = true;
		btnCancel.interactable = false;
		txInfo.text = "Info: Server Address " + network.networkAddress + " with port " + network.networkPort;
	}
	
	private void ConnectionError(NetworkMessage netMsg)
	{
		//network.StopClient();
		//txInfo.text = "Info: Disconnect From Server";
		BackToMain();
	}
	
	public void StartGame()
	{
		multiplayerPanel.transform.localPosition = new Vector3(-1500, 0, 0);
	}
	
	public void BackToMain()
	{
		network.StopHost();
		SceneManager.LoadScene("MainGame");
	}
	
	public void BackToMenu()
	{
		network.StopHost();
		SceneManager.LoadScene("MenuGame");
	}
}

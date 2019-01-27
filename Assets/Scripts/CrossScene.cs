using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossScene : MonoBehaviour {
	public string StartScene;
	public string CreditsScene;
	public string MenuScene;
	public string QuitScene;
	
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			Debug.Log("Quit Scene: " + QuitScene);
			SceneManager.LoadScene(QuitScene);
		}
	}
	
public void StartButtonClicked()
{
	SceneManager.LoadScene("MainGame");
}
public void CreditsButtonClicked()
{
	SceneManager.LoadScene("CreditsGame");
}
public void BackToMenuClicked()
{
	SceneManager.LoadScene("MenuGame");
}
public void QuitButtonClicked()
{
	Application.Quit();
}
}

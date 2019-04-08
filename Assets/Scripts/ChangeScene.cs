using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ChangeGameScene()
	{
		SceneManager.LoadScene("how to play");
	}
    public void gotomain()
    {
        SceneManager.LoadScene("main scene");
    }
    public void gotoGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}

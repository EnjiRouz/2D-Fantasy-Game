using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour {

    public string scene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGame ()
    {
        SceneManager.LoadScene(3);
    }

    public void Load()
    {
       scene = SceneManager.GetActiveScene().name;
    }

    public void Exit()
    {
        Application.Quit();
    }

}

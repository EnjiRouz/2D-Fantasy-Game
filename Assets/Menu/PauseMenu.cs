using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool paused = false;
    public GameObject PauseUI;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Time.timeScale = 0;
                paused = true;
                PauseUI.SetActive(true);
                CharacterAnimationController.anim.SetBool("StopMovement", true);
            }
            else
            {
                Resume();
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        paused = false;
        PauseUI.SetActive(false);
        CharacterAnimationController.anim.SetBool("StopMovement", false);
    }

  /*  public void Restart()
    {
        SceneManager.LoadScene(nextLevel);
    }*/
    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
/*
public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;

    private bool paused = false;

    void Start()
    {
        PauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }
        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        paused = false;
    }
}*/


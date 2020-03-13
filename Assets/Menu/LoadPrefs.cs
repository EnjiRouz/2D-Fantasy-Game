using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPrefs : MonoBehaviour {

    public string scene;
    public int HP_hero;
    public int Strength_hero;
    public int Magic_hero;
    public int Intellegence_hero;
    public int Score_hero;
    public int Experience_hero;

    private string LostLevel;

    void Start()
    {
        //LostLevel = PlayerPrefs.GetString("LostLevel");
    }
  
	
	// Update is called once per frame
	public void Load ()
    {
        LostLevel =  SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene(LostLevel);
        // scene = PlayerPrefs.GetString(SceneManager.GetActiveScene().name);
        //if (Input.GetKeyDown(KeyCode.M))
        // {
        if (PlayerPrefs.HasKey("HP"))
            {
                HP_hero = PlayerPrefs.GetInt("HP");
            }
            else HP_hero = 0;

            if (PlayerPrefs.HasKey("Strength"))
            {
                Strength_hero = PlayerPrefs.GetInt("Strength");
            }
            else Strength_hero = 0;

            if (PlayerPrefs.HasKey("Magic"))
            {
                Magic_hero = PlayerPrefs.GetInt("Magic");
            }
            else Magic_hero = 0;

            if (PlayerPrefs.HasKey("Intellegence"))
            {
                Intellegence_hero = PlayerPrefs.GetInt("Intellegence");
            }
            else Intellegence_hero = 0;

            if (PlayerPrefs.HasKey("Score"))
            {
                Score_hero = PlayerPrefs.GetInt("Score");
            }
            else Score_hero = 0;

            if (PlayerPrefs.HasKey("Experience"))
            {
                Experience_hero = PlayerPrefs.GetInt("Experience");
            }
            else Experience_hero = 0;

            Debug.Log("Load");
            
            

       // }
            
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePrefs : MonoBehaviour {

	// Use this for initialization
	void Start () {
       	
	}
   
   public void Save()
    {
        PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        PlayerPrefs.SetInt("HP", PlayerPrefs.GetInt("Hels"));   //здоровье
        PlayerPrefs.SetInt("Strength", PlayerPrefs.GetInt("Streng"));   //сила
        PlayerPrefs.SetInt("Magic", PlayerPrefs.GetInt("Mana"));  // магия
        PlayerPrefs.SetInt("Intellegence", PlayerPrefs.GetInt("Intel"));   //интеллект
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Point"));   //очки прокачки
        PlayerPrefs.SetInt("Experience", PlayerPrefs.GetInt("EndExp"));    //опыт
        PlayerPrefs.Save();
        // PlayerPrefs.SetInt("LastLevelLoaded", PlayerPrefs.GetInt("LastLevelLoaded"));
        //PlayerPrefs.Set
       // PlayerPrefs.Save(player.transformers
        Debug.Log("Save");
        //
       
       //
        //PlayerPrefs.SetString("LostLevel", "");
        // Debug.Log("SaveScene");
    }

	
	// Update is called once per frame
	void Update () {
        //PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
       // PlayerPrefs.Save();
    }
}

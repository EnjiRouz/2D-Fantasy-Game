
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {
    [SerializeField] private string nextLevel;

    /// смена сцены
    void OnTriggerEnter2D(Collider2D other)    {
        if (other.CompareTag("Character"))
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
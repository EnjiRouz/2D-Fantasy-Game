using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public GameObject stone;
    public Transform throwPosition;
    public int delay;
    private int currentFrame=0;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        currentFrame++;
        if(currentFrame >= delay)
        {
            currentFrame = 0;
            ThrowStone();
        }
	}

    /// <summary>
    /// Бросок камней в игрока
    /// </summary>
    public void ThrowStone()
    {
        GameObject newStone = GameObject.Instantiate<GameObject>(stone, throwPosition.position, Quaternion.identity);
        newStone.SetActive(true);
        Throws throws = newStone.GetComponent<Throws>();
        //throws.enabled = true;
        newStone.GetComponent<Collider2D>().tag = "ThrowObject";
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float x = player.transform.position.x;
        throws.Initialize(new Vector2(1, 0));
        
    }
}

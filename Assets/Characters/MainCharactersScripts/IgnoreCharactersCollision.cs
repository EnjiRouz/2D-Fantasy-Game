using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCharactersCollision : MonoBehaviour {

    [SerializeField]
    private Collider2D other;

    /// используется для того, чтобы враг и игрок не толкали на сцене друг друга, 
    /// но сталкивались с коллизией сцен (может использоваться и в др ситуациях)
    private void Awake ()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
    }

}

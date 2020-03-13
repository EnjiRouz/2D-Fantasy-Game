using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    [SerializeField]
    private Enemy enemy;

    /// переменные для рисования луча и сообщения о нахождении цели
    public Transform sightStart, sightEnd;
    public bool spotted = false;

	/// рисование луча и реагирование ИИ
	void Update () {
        Raycasting();
        Behaviours();
    }

    /// рисование луча и определение цели с помощью слоя, на котором она должна быть
    void Raycasting()
    {
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));

    }

    /// поведение ИИ
    void Behaviours()
    {
        /// нахождение цели, если она в зоне видимости
        if (spotted)
        {
            enemy.Target = FindObjectOfType<CharacterAnimationController>().gameObject;
        }
        /// потеря цели из виду
        else
        {
            enemy.Target = null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Применяется только для рычага (ветки) на локации QuestSystemDemo
// Корректная работоспособность на других спрайтах не гарантируется
public class LeverActivation : MonoBehaviour
{
    bool isActivation = false;
    void OnTriggerStay2D(Collider2D player)
    {
        if (player.CompareTag("Character") && !isActivation && Input.GetKeyDown(KeyCode.E))
        {
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y + (1.7f * transform.localScale.y), transform.position.z);
            isActivation = !isActivation;
        }
    }
}

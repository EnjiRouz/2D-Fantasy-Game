using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

   public Dialogs[] dialog;

   /// триггер запуска диалога с блокировкой движения главного героя (пока что только его)
   public void OnTriggerStay2D(Collider2D other)
    {
        if ((other.CompareTag("Character")) && (Input.GetKeyDown(KeyCode.E)))
        {
            FindObjectOfType<DialogManager>().StartDialog(dialog);
            CharacterAnimationController.anim.SetBool("StopMovement", true);
        }
    }
}
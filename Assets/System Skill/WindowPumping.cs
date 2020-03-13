using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowPumping : MonoBehaviour {

    public Animator animator;

    public void ShowWindow()
    {
        animator.SetBool("IsDialogOpen", true);     
    }

    public void ConcealWindow()
    {
       animator.SetBool("IsDialogOpen", false);
       CharacterAnimationController.anim.SetBool("StopMovement", false);
       return;
    }
}

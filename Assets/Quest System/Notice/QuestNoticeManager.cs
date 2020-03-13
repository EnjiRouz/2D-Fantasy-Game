using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNoticeManager : MonoBehaviour {

    public Text NameTextBox;
    public Text ActionTextBox;

    public Animator animator;
    public AnimationClip z;

    // начать диалог
    public void ShowNotice(QuestNotice notice)
    {
        NameTextBox.text = notice.Name;
        ActionTextBox.text = notice.ActionText;
        animator.SetTrigger("Show");
    }
}

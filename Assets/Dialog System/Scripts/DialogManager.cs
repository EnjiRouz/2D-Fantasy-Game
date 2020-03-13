using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    /// переменные для отображения текста на экране 
    public Text nameText_Box;
    public Text dialogText_Box;

    public Animator animator;

    private Queue<Dialogs> dialogs;

    /// инициализация очереди из диалоговых фраз
    void Start () {
        dialogs = new Queue<Dialogs>();
    }

    /// начало диалога
    public void StartDialog(Dialogs[] dialog)
    {
        animator.SetBool("IsDialogOpen", true);
        dialogs.Clear();
        foreach (Dialogs dial in dialog)
        {
            dialogs.Enqueue(dial);
        }
        DisplayNextPhrase();
    }

    /// функция для кнопки продолжения (стрелочки) в диалогах
    public void DisplayNextPhrase()
    {
        if (dialogs.Count == 0)
        {
            EndDialog();
            return;
        }
        Dialogs dialog = dialogs.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialog));
    }

    /// обновление текста на экране
    IEnumerator TypeSentence(Dialogs dialog)
    {
        nameText_Box.text = dialog.npc_name;
        dialogText_Box.text = dialog.phrases;
        yield return null;
    }

    /// завершение диалога
    void EndDialog()
    {
        animator.SetBool("IsDialogOpen", false);
        CharacterAnimationController.anim.SetBool("StopMovement", false);
    }
}
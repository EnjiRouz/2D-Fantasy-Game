using UnityEngine;

/// <summary>
/// Основной класс квестовой системы
/// Отвечает за выдачу и завершение квеста
/// </summary>
public class StartQuestPoint : MonoBehaviour {
    public Dialogs[] dialog;
    public QuestInfo Quest;

    [SerializeField]
    [Header("Условие активации")]
    public QuestActivators.Activators QuestActivator;

    private bool isGenerated = false;

    void OnTriggerStay2D(Collider2D other)    {
        if ((other.CompareTag("Character")) && QuestActivators.Check(QuestActivator))
        {
            // если точка уже пройдена (квест уже выполнен ранее)
            if (Quest.IsComplete || PlayerQuests.CompletedQuests.ContainsKey(Quest.Name)) 
            {
                if (!isGenerated)
                {
                    isGenerated = true;
                    if (Quest.gold != 0)
                    {
                        var inventory = FindObjectOfType<Inventory>();
                        inventory.money += Quest.gold;

                        FindObjectOfType<QuestNoticeManager>().ShowNotice(
                            new QuestNotice("Получена награда:", " + " + Quest.gold + " монет"));
                        Debug.Log(inventory.money + " награда получена");
                    }
                    if (Quest.exp != 0)
                    {

                        var experience = FindObjectOfType<SystemPumping>();
                        experience.point += Quest.exp;

                        FindObjectOfType<QuestNoticeManager>().ShowNotice(
                            new QuestNotice("Получена награда:", " + " + Quest.exp + " очков опыта"));
                        Debug.Log(experience.point + " награда получена");

                    }
                }
                return;
            }
            // если квест ещё не выполнен
            if (PlayerQuests.CurrentQests.ContainsKey(Quest.Name))
            {
                if (!TryCompleteQuest())
                {
                    FindObjectOfType<QuestNoticeManager>().ShowNotice(
                        new QuestNotice(Quest.Name, Quest.ShortDescription));
                }
            }
            else
            {
                GiveQuest(other);
            }
        }
    }

    private bool TryCompleteQuest()
    {
        if (Quest.TryСomplete())
        {
            // если квест выполнен, происходит его "сдача" (завершение)
            PlayerQuests.CompletedQuests[Quest.Name] = Quest;
            PlayerQuests.CurrentQests.Remove(Quest.Name);
            FindObjectOfType<QuestNoticeManager>().ShowNotice(new QuestNotice(Quest.Name, "Квест завершён!"));
            return true;
        }
        return false;
    }

    private void GiveQuest(Collider2D other)
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
        CharacterAnimationController.anim.SetBool("StopMovement", true);

        PlayerQuests.CurrentQests.Add(Quest.Name, Quest);
        FindObjectOfType<QuestManager>().PointsSetting(Quest.Name);

        FindObjectOfType<QuestNoticeManager>().ShowNotice(
                new QuestNotice(Quest.Name, "Получен новый квест: " + Quest.ShortDescription)
                );
        }
    }

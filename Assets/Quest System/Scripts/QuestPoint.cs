using UnityEngine;

/// <summary>
/// Точка квеста, которая должна иметь статус IsComplete = True для завершения квеста
/// </summary>
public class QuestPoint : MonoBehaviour
{
    private int complete;
    public bool IsComplete { get; private set; }
    public QuestInfo Quest { get; set; }
    public string QuestName; // необходим для нахлждения QuestManager'ом (необязателен для дочерних точек)

    [SerializeField] private QuestActivators.Activators activator;
    [Header("Сообщение уведомления при выполнении")]
    [SerializeField] private string notification = null;
    private IRequirement[] requirements;

    [Header("Текст задания в уведомлении (не обязателен)")]
    public string TaskNotification = "";

    void OnEnable()
    {
        IsComplete = false;
    }

    private void Start()
    {
        requirements = GetComponents<IRequirement>();
    }

    private void OnTriggerStay2D(Collider2D player)
    {
        TryComplete(player);
    }
    
    private void TryComplete(Collider2D player)
    {
        if (IsComplete)
        {
            return;
        }

        bool isActivated;
        isActivated = QuestActivators.Check(activator);

        if (player.CompareTag("Character") && isActivated)
        {
            if (!CheckRequirements())
            {
                return;
            }
            var item = GetComponent<Item>();
            if (item != null)
            {
                item.pickable = true;
            }
            IsComplete = true;

            // обновление состояние квеста и уведомление игрока
            ShowNotice(notification);
            if (Quest != null)
            {
                Quest.TryСomplete();
            }
        }
    }

    private bool CheckRequirements()
    {
        //requirements = GetComponents<IRequirement>();
        /*foreach (var i in requirements)
            {
                if (!i.IsComplete())
                {
                    if (i.Notification != "")
                    {
                        ShowNotice(i.Notification);
                    }
                    return false;
                }
            }
        return true;*/

        foreach (var i in requirements)
        {
            if (!i.IsComplete() && complete == 1)
            {
                if (i.Notification != "")
                {
                    //QuestPoint.ShowNotice(i.Notification);
                    FindObjectOfType<QuestPoint>().ShowNotice(i.Notification);
                }
                return false;
            }
            complete++;
        }
        return true;
    }

    private void ShowNotice(string notification)
    {
        if (notification != "" && Quest != null)
        {
            FindObjectOfType<QuestNoticeManager>().ShowNotice(
                new QuestNotice(Quest.Name, notification)
                );
        }
    }
}

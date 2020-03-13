using UnityEngine;

public class ChildQuestPoints : MonoBehaviour, IRequirement
{
    public string Notification { get; private set; }

    [SerializeField]
    private QuestPoint[] questPoints = null;

    public bool IsComplete()
    {
        var quest = GetComponent<QuestPoint>().Quest;
        foreach (var i in questPoints)
        {
            if (i == null)
            {
                throw new System.ArgumentNullException("Элемент в массиве дочерних точек равен null");
            }
            if (!i.IsComplete)
            {
                Notification = i.TaskNotification;
                if (i.Quest == null)
                {
                    i.Quest = GetComponent<QuestPoint>().Quest;
                }
                if (i.GetComponent<BoxCollider2D>().enabled == false)
                {
                    i.GetComponent<BoxCollider2D>().enabled = true;
                }
                return false;
            }
        }
        Notification = "";
        return true;
    }
}

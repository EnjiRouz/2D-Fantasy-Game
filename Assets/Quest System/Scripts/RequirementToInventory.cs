using UnityEngine;

public class RequirementToInventory : MonoBehaviour, IRequirement
{
    [SerializeField] private string thingName;
    [Header("Сообщение уведомления при не выполнении условия")]
    [SerializeField] private string notification = "";
    public string Notification { get { return notification; } }

    public bool IsComplete()
    {
        var inventory = FindObjectOfType<Inventory>();
        if ((inventory.Peek(ItemType.QuestItem) != null) )//|| (FindObjectOfType<RequirementToKill>().IsComplete() == true))
            {
            return inventory.Peek(ItemType.QuestItem).name.ToLower() == thingName.ToLower();
        }
        return false;
    }
}
using UnityEngine;

public class RequirementToKill : MonoBehaviour, IRequirement
{
    [SerializeField]   private GameObject Target;
    [Header("Сообщение уведомления при не выполнении условия")]
    [SerializeField] private string notification = "";
    public string Notification { get { return notification; } }

    void Start()
    {
        Target.GetComponent<Enemy>().Dead += new EnemyIsDead(RemoveTarget);
    }

    public bool IsComplete()
    {
        if ((Target == null) )//||(FindObjectOfType<RequirementToInventory>().IsComplete()==true))
        {
            return true;
        }
        return false;
    }

    public void RemoveTarget()
    {
        Target = null;
    }
}
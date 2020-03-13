using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour {

    [SerializeField]
    [Header("Условие перехода")]
    private QuestActivators.Activators activator;


    [SerializeField]
    private Transform teleportTo;
    public GameObject objectToMove;

    [SerializeField] private bool isBlockedByQuest;


    void OnTriggerStay2D(Collider2D other)
    {
        var questManager = GetComponent<StartQuestPoint>();
        if (other.CompareTag("Character") && (!isBlockedByQuest || questManager.Quest.IsComplete) && QuestActivators.Check(activator))
        {
            objectToMove.transform.position = new Vector2(teleportTo.position.x, teleportTo.position.y);
        }
    }
}

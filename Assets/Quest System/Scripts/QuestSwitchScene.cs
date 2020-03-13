using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestSwitchScene : MonoBehaviour {
    [SerializeField] private string nextLevel;

    [SerializeField]
    [Header("Условие перехода")]
    private QuestActivators.Activators activator;

/*
    [SerializeField]
    public float x;
    [SerializeField]
    public float y;
    [SerializeField]
    public float z;
    public GameObject objectToMove;*/

    [SerializeField] private bool isBlockedByQuest;


    void OnTriggerStay2D(Collider2D other)
    {
        var questManager = GetComponent<StartQuestPoint>();
        if (other.CompareTag("Character") && (!isBlockedByQuest || questManager.Quest.IsComplete) && QuestActivators.Check(activator))
        {
                SceneManager.LoadScene(nextLevel);
                /*objectToMove.transform.position = new Vector3(FindObjectOfType<QuestSwitchScene>().x,
                                                    FindObjectOfType<QuestSwitchScene>().y,
                                                    FindObjectOfType<QuestSwitchScene>().z);*/
        }
    }
}

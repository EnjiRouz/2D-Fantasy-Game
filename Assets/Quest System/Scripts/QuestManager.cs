using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    private Dictionary<string, QuestInfo> playerQuests;
    private QuestPoint[] sceneQuests;

    void Start () {
        playerQuests = PlayerQuests.CurrentQests;
        sceneQuests = FindObjectsOfType<QuestPoint>();

        foreach (var i in sceneQuests)
        {
            if (playerQuests.ContainsKey(i.QuestName))
            {
                i.Quest = playerQuests[i.QuestName];
            }
        }
    }

    public void PointsSetting(string questName)
    {
        if (playerQuests.ContainsKey(questName))
        {
            foreach (var i in sceneQuests)
            {
                if (i.QuestName != questName)
                {
                    continue;
                }
                var collider = i.gameObject.GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    collider.enabled = true;
                }
                i.Quest = playerQuests[i.QuestName];
            }
        }
    }
}

using UnityEngine;

[System.Serializable]
public class QuestInfo {
    public string Name;
    public string ShortDescription;
    [TextArea(1, 20)]
    public string FullDescription; // может меняться по ходу выполнения
    public bool IsComplete = false;

    // нагррада за прохождение квеста (золото/очки опыта)
    public int gold;
    public int exp;
    
    // токи квеста которые должны быть активированы
    public QuestPoint[] Points;

    public bool TryСomplete()
    {
        var check = true;
        foreach (var point in Points)
        {
            if (!point.IsComplete)
            {
                check = false;
                break;
            }
        }
        IsComplete = check;
        return check;
    }

    public QuestInfo()
    {
        IsComplete = false;
    }
}
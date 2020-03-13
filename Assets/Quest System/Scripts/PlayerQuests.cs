using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Список активных и завершённых квестов
/// </summary>
static public class PlayerQuests {
    public static Dictionary<string, QuestInfo> CurrentQests = new Dictionary<string, QuestInfo>();
    public static Dictionary<string, QuestInfo> CompletedQuests = new Dictionary<string, QuestInfo>();
}

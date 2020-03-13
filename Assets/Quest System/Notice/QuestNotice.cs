[System.Serializable]
public class QuestNotice{
    public string Name;
    public string ActionText;

    public QuestNotice(string name, string action)
    {
        Name = name;
        ActionText = action;
    }
}

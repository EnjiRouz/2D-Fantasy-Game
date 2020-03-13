public interface IRequirement
{
    string Notification { get; }
    bool IsComplete();
}

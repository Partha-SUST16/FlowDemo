using MediatR;

namespace Models.Event;

public class UpdateTimeTagEvent : INotification
{
    public UpdateTimeTagEvent(string id, string newTag, string previousTag)
    {
        Id = id;
        NewTag = newTag;
        PreviousTag = previousTag;
    }
    public string Id { get; }
    public string NewTag { get; }
    public string PreviousTag { get; }
}
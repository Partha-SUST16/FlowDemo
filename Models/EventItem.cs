using Models.Primitives;

namespace Models;


public class EventItem : Entity
{
    public EventItem(string id, 
                     string description, 
                     double latitude, 
                     double longitude,
                     DateTime startTime,
                     DateTime endTime,
                     EventType eventType,
                     Creator creator) : base(id)
    {
        Description = description;
        Latitude = latitude;
        Longitude = longitude;
        StartTime = startTime;
        EndTime = endTime;
        EventType = eventType;
        Creator = creator;
        IsOpenForEveryOne = true;
    }
    public string Description { get; protected set; }
    public double Latitude { get; protected set; }
    public double Longitude { get; protected set; }
    public DateTime EndTime { get; protected set; }
    public DateTime StartTime { get; protected set; }
    public string GeoHash { get; protected set; }
    public EventType EventType { get; protected set; }
    public bool IsOpenForEveryOne { get; protected set; }
    public string TimeTag { get; protected set; }
    public Creator Creator { get; protected set; }

    public void UpdateTimeTag(string tag)
    {
        TimeTag = tag;
    }

    public void SetGeoHash(string hashValue)
    {
        GeoHash = hashValue;
    }
}
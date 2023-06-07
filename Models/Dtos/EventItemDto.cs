namespace Models.Dtos;

public class EventItemDto
{
    public string Id { get; set; }
    public string Description { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime StartTime { get; set; }
    public string GeoHash { get; set; }
    public EventType EventType { get; set; }
    public bool IsOpenForEveryOne { get; set; }
    public string TimeTag;
    public CreatorDto Creator { get; set; }
}
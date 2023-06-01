namespace Models;

public class EventTimeRangeConfig
{
    public int Hours { get; set; }
}

public enum QueryTimeTag
{
    MORNING,
    AFTERNOON,
    EVENING,
    NIGHT,
    LIVE,
    TODAY,
    TOMORROW,
    WEEKEND,
    PAST
}
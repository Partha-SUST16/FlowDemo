namespace Business.Interfaces;

public interface ITimeService
{
    DateTime GetEndTime(DateTime queryTime);
    Tuple<DateTime, DateTime> GetTimeRangeFromTimeQueryTag(string tag);
}
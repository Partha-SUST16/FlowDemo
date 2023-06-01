using Business.Interfaces;
using Models;
using Newtonsoft.Json.Linq;

namespace Business.Services;

public class TimeService : ITimeService
{
    private EventTimeRangeConfig workDay;
    private EventTimeRangeConfig weekend;

    public TimeService()
    {
        LoadConfiguration();
    }

    private void LoadConfiguration()
    {
        workDay = LoadConfigFromFile<EventTimeRangeConfig>("WorkDay");
        weekend = LoadConfigFromFile<EventTimeRangeConfig>("Weekend");
    }
    private T LoadConfigFromFile<T>(string key)
    {
        
        string configFilePath = Directory.GetCurrentDirectory() + "/Resources/TimeRange.json";

        if (!File.Exists(configFilePath))
        {
            throw new Exception("File Not Found");
        }
        string jsonConfig = File.ReadAllText(configFilePath);

        var configObject = JObject.Parse(jsonConfig);

        JToken configToken = configObject[key];

        T config = configToken.ToObject<T>();

        return config;
    }
    public DateTime GetEndTime(DateTime queryTime)
    {
        DayOfWeek requestedDayOfWeek = queryTime.DayOfWeek;

        return requestedDayOfWeek is >= DayOfWeek.Monday and <= DayOfWeek.Thursday
            ? queryTime.Add(new TimeSpan(0, workDay.Hours, 0))
            : queryTime.Add(new TimeSpan(0, weekend.Hours, 0));
    }

    public Tuple<DateTime, DateTime> GetTimeRangeFromTimeQueryTag(string tag)
    {
        const bool shouldIgnoreCase = true;
        if (!Enum.TryParse<QueryTimeTag>(tag, shouldIgnoreCase, out var queryTimeTag))
        {
            throw new ArgumentOutOfRangeException("Invalid Query Tag for Time Range");
        }

        switch (queryTimeTag)
        {
            case QueryTimeTag.LIVE:
                return new Tuple<DateTime, DateTime>(DateTime.UtcNow, DateTime.MinValue) ;
            case QueryTimeTag.TODAY:
                return new Tuple<DateTime, DateTime>(DateTime.UtcNow.Date,
                    DateTime.UtcNow.Date.AddDays(1).AddTicks(-1));
            case QueryTimeTag.TOMORROW:
                return new Tuple<DateTime, DateTime>(DateTime.UtcNow.Date.AddDays(1),
                    DateTime.UtcNow.Date.AddDays(2).AddTicks(-1));
            case QueryTimeTag.AFTERNOON:
                TimeSpan afternoonStart = TimeSpan.FromHours(12.01);
                TimeSpan afternoonEnd = TimeSpan.FromHours(17.99);
                return new Tuple<DateTime, DateTime>(DateTime.UtcNow.Add(afternoonStart), DateTime.UtcNow.Add(afternoonEnd));
            case QueryTimeTag.WEEKEND:
                var today = DateTime.UtcNow;
                var dayOfWeek = today.DayOfWeek;
                var daysToAdd = (DayOfWeek.Friday - dayOfWeek + 7) % 7;
                var nextFriday = today.AddDays(daysToAdd);
                var endOfSaturday = nextFriday.AddDays(2).AddTicks(-1);
                return new Tuple<DateTime, DateTime>(nextFriday, endOfSaturday);
            case QueryTimeTag.NIGHT:
                var hoursToAdd = (22 - DateTime.UtcNow.Hour + 24) % 24;
                if (hoursToAdd > 22) return new Tuple<DateTime, DateTime>(DateTime.UtcNow, DateTime.MinValue) ;
                return new Tuple<DateTime, DateTime>(DateTime.UtcNow.AddHours(hoursToAdd),
                    DateTime.UtcNow.AddHours(hoursToAdd + 5));
            case QueryTimeTag.MORNING:
                TimeSpan morningTime = TimeSpan.FromHours(6);
                TimeSpan noonTime = TimeSpan.FromHours(12);
                return new Tuple<DateTime, DateTime>(DateTime.UtcNow.Add(morningTime), DateTime.UtcNow.Add(noonTime));
            case QueryTimeTag.EVENING:
                var timeRemainingForEvening = (18 - DateTime.UtcNow.Hour + 24) % 24;
                if (timeRemainingForEvening > 18) return new Tuple<DateTime, DateTime>(DateTime.UtcNow, DateTime.MinValue);
                return new Tuple<DateTime, DateTime>(DateTime.UtcNow.AddHours(timeRemainingForEvening),
                    DateTime.UtcNow.AddHours(timeRemainingForEvening + 5));
            default:
                throw new ArgumentOutOfRangeException("Invalid Query Tag for Time Range");
        }
    }
}
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
}
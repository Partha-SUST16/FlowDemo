using Business.Interfaces;
using Geohash;
using Models;

namespace Business.Services;

public class EventService : IEventService
{
    private readonly IDbService _dbService;
    private readonly ITimeService _timeService;
    private readonly Geohasher _geohasher;
    private const int GEOHASH_PRECISION = 5; //4.9KM^2

    public EventService(IDbService dbService, ITimeService timeService)
    {
        _dbService = dbService;
        _geohasher = new Geohasher();
        _timeService = timeService;
    }
    public EventItem GetEventById(string eventId)
    {
        if (string.IsNullOrEmpty(eventId)) 
            return null;
        return _dbService.GetById(eventId);
    }

    public List<EventItem> GetEventsByType(int type)
    {
        var eventItems = new List<EventItem>();
        var allEvents = _dbService.GetAll();
        eventItems = allEvents.Where(item => item.EventType == (EventType)type).ToList();
        return eventItems;
    }

    public List<EventItem> GetTopEvents(DateTime date)
    {
        //TODO:: Do something to rank the top events for the day
        //For now just return all the event of that date
        var allEvents = _dbService.GetAll();
        var eventItems = allEvents.Where(item => item.StartTime.Date == date.Date).ToList();
        return eventItems;
    }

    public List<EventItem> GetEventsByTime(DateTime startTime, DateTime endTime)
    {
        startTime = startTime.ToUniversalTime();
        endTime = endTime.ToUniversalTime();
        var allEvents = _dbService.GetAll();
        var eventItems = allEvents.Where(item => item.StartTime >= startTime && item.EndTime <= endTime);
        return eventItems.ToList();
    }

    public List<EventItem> GetEventsNearUser(double lat, double lon, int type = -1)
    {
        string geoHashCode = GetGeoHash(lat, lon);
        var eventItems = _dbService.GetAll().Where(item => string.Equals(geoHashCode, item.GeoHash));
        if (type != -1)
        {
            eventItems = eventItems.Where(item => item.EventType == (EventType)type);
        }
        return eventItems.ToList();
    }

    public List<EventItem> GetEventsNearUser(string cityName)
    {
        throw new NotImplementedException();
    }

    public EventItem SaveEvent(EventItem eventItem)
    {
        if (eventItem == null) return null;
        eventItem.StartTime = eventItem.StartTime.ToUniversalTime();
        eventItem.EndTime = eventItem.EndTime.ToUniversalTime();
        eventItem.GeoHash = GetGeoHash(eventItem.Latitude, eventItem.Longitude);
        return _dbService.Save(eventItem);
    }

    public List<EventItem> GetEventsByCategoryList(List<int> categories)
    {
        if (categories == null || categories.Count == 0) return new List<EventItem>();
        var allEvents = _dbService.GetAll();
        var matchingItems = allEvents.Where(item => categories.Contains((int)item.EventType));
        return matchingItems.ToList();
    }

    public List<EventItem> GetEventsNearFuture(DateTime startTime)
    {
        var endTime = _timeService.GetEndTime(startTime);
        var futureEvents = GetEventsByTime(startTime, endTime);
        return futureEvents;
    }

    private string GetGeoHash(double latitude, double longitude)
    {
        return _geohasher.Encode(latitude, longitude, GEOHASH_PRECISION);
    }
}
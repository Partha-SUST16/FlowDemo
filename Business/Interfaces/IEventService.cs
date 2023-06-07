using Models;
using Models.Dtos;

namespace Business.Interfaces;

public interface IEventService
{
    EventItem GetEventById(string eventId);
    List<EventItem> GetEventsByType(int type);
    List<EventItem> GetTopEvents(DateTime date);
    List<EventItem> GetEventsByTime(DateTime startTime, DateTime endTime);
    List<EventItem> GetEventsNearUser(double lat, double lon, int type = -1);
    List<EventItem> GetEventsNearUser(string cityName);
    EventItem SaveEvent(EventItemDto eventItemDto);
    List<EventItem> GetEventsByCategoryList(List<int> categories);
    List<EventItem> GetEventsNearFuture(DateTime startTime);
    List<EventItem> GetEventsByTimeTag(string tag);
}
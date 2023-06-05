using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace FlowDemo.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }
    
    [HttpGet]
    public ActionResult<EventItem> GetEventItemById(string id)
    {
        return _eventService.GetEventById(id);
    }

    [HttpPost("save")]
    public ActionResult<EventItem> SaveEventItem(EventItem item)
    {
        return _eventService.SaveEvent(item);
    }

    [HttpGet("getByDate")]
    public ActionResult<List<EventItem>> GetEventListByDate(DateTime startTime, DateTime endTime)
    {
        return _eventService.GetEventsByTime(startTime, endTime);
    }

    [HttpGet("getByLocation")]
    public ActionResult<List<EventItem>> GetEventsByLocation(double lat, double lon, int type = -1)
    {
        return _eventService.GetEventsNearUser(lat, lon, type);
    }

    [HttpGet("getTopEvents")]
    public ActionResult<List<EventItem>> GetTopEvents(DateTime date)
    {
        return _eventService.GetTopEvents(date);
    }

    [HttpPost("getByCategory")]
    public ActionResult<List<EventItem>> GetEventsByCategories(List<int> categories)
    {
        return _eventService.GetEventsByCategoryList(categories);
    }
    
    [HttpGet("getEventsHappeningSoon")]
    public ActionResult<List<EventItem>> GetEventsHappeningSoon(DateTime queryTime)
    {
        return _eventService.GetEventsNearFuture(queryTime);
    }

    [HttpGet("getByTimeTag")]
    public ActionResult<List<EventItem>> GetEventsByTimeTag(string timeTag)
    {
        return _eventService.GetEventsByTimeTag(timeTag);
    }

}
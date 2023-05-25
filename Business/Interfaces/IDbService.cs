using Models;

namespace Business.Interfaces;

public interface IDbService
{
    EventItem Save(EventItem item);
    void DeleteById(string id);
    EventItem GetById(string id);
    List<EventItem> GetAll();
}
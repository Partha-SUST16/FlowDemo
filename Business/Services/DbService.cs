using Business.Interfaces;
using Models;

namespace Business.Services;

public class DbService : IDbService
{
    private List<EventItem> Collections { get; set; }

    public DbService()
    {
        Collections = new List<EventItem>();
    }
    public EventItem Save(EventItem item)
    {
        if (item == null) return null;
        if (string.IsNullOrEmpty(item.Id))
            item.Id = Convert.ToString(Collections.Count + 1);
        Collections.Add(item);
        return item;
    }

    public void DeleteById(string id)
    {
        Collections.Remove(Collections.Find(item => item.Id == id));
    }

    public EventItem GetById(string id)
    {
        return Collections.FirstOrDefault(item => item.Id == id);
    }

    public List<EventItem> GetAll()
    {
        return Collections;
    }
}
using MediatR;

namespace Models.Event;

public class SetGeoHashEvent: INotification
{
    public SetGeoHashEvent(string id, double lat,double lon, string hashedValue)
    {
        Id = id;
        Longitude = lon;
        Latitude = lat;
        GeoHash = hashedValue;
    }
    public string Id { get; }
    public double Latitude { get; }
    public double Longitude { get; }
    public string GeoHash { get; }
}
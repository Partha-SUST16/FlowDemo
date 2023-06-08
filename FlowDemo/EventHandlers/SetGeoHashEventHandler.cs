using MediatR;
using Models.Event;

namespace FlowDemo.EventHandlers;

public class SetGeoHashEventHandler<ISetGeoHashEvent> : INotificationHandler<ISetGeoHashEvent> 
    where ISetGeoHashEvent : SetGeoHashEvent
{
        private readonly ILogger<SetGeoHashEventHandler<ISetGeoHashEvent>> _logger;

        public SetGeoHashEventHandler(ILogger<SetGeoHashEventHandler<ISetGeoHashEvent>> logger)
        {
            _logger = logger;
        }
        public async Task Handle(ISetGeoHashEvent notification, CancellationToken cancellationToken)
        {
            var message =
                $"GeoHash set for Event Item id: {notification.Id}, latitude: {notification.Latitude}, longitude: {notification.Longitude} to {notification.GeoHash}";
            _logger.LogInformation(message);
        }
}
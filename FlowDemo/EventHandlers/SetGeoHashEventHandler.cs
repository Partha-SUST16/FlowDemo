using MediatR;
using Models.Event;

namespace FlowDemo.EventHandlers;

public class SetGeoHashEventHandler : INotificationHandler<SetGeoHashEvent>
{
        private readonly ILogger<UpdateTimeTagEventHandler> _logger;

        public SetGeoHashEventHandler(ILogger<UpdateTimeTagEventHandler> logger)
        {
            _logger = logger;
        }
        public async Task Handle(SetGeoHashEvent notification, CancellationToken cancellationToken)
        {
            var message =
                $"GeoHash set for Event Item id: {notification.Id}, latitude: {notification.Latitude}, longitude: {notification.Longitude} to {notification.GeoHash}";
            _logger.LogInformation(message);
        }
}
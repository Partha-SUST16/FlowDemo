using MediatR;
using Models.Event;

namespace FlowDemo.EventHandlers;

public class UpdateTimeTagEventHandler: INotificationHandler<UpdateTimeTagEvent>
{
    private readonly ILogger<UpdateTimeTagEventHandler> _logger;

    public UpdateTimeTagEventHandler(ILogger<UpdateTimeTagEventHandler> logger)
    {
        _logger = logger;
    }
    public async Task Handle(UpdateTimeTagEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Event time tag was changed for eventId: {notification.Id} from {notification.PreviousTag} to {notification.NewTag}");
    }
}
using MediatR;
using Models.Event;

namespace FlowDemo.EventHandlers;

public class UpdateTimeTagEventHandler<IUpdateTimeTagEvent>: INotificationHandler<IUpdateTimeTagEvent> where IUpdateTimeTagEvent : UpdateTimeTagEvent
{
    private readonly ILogger<UpdateTimeTagEventHandler<IUpdateTimeTagEvent>> _logger;

    public UpdateTimeTagEventHandler(ILogger<UpdateTimeTagEventHandler<IUpdateTimeTagEvent>> logger)
    {
        _logger = logger;
    }
    public async Task Handle(IUpdateTimeTagEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Event time tag was changed for eventId: {notification.Id} from {notification.PreviousTag} to {notification.NewTag}");
    }
}
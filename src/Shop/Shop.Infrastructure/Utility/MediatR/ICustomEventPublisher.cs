namespace Shop.Infrastructure.Utility.MediatR;

public interface ICustomEventPublisher
{
    Task Publish<TNotification>(TNotification notification);
    Task Publish<TNotification>(TNotification notification, PublishStrategy strategy);
    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken);
    Task Publish<TNotification>(TNotification notification, PublishStrategy strategy, CancellationToken cancellationToken);
}
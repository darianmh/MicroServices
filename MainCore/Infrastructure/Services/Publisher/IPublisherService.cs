namespace Infrastructure.Services.Publisher;

public interface IPublisherService
{
    Task<bool> Publish(int number, CancellationToken cancellationToken);
}
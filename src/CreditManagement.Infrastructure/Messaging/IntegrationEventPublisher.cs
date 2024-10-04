using CreditManagement.Application.Abstractions.Messaging;

namespace CreditManagement.Infrastructure.Messaging;

public class IntegrationEventPublisher : IIntegrationEventPublisher, IDisposable
{
    public void Dispose()
    {
    }

    public void Publish(IIntegrationEvent integrationEvent)
    {
    }
}
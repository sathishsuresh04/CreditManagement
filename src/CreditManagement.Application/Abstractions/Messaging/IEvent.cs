using MediatR;

namespace CreditManagement.Application.Abstractions.Messaging;

/// <summary>
///     Represents the event interface.
/// </summary>
public interface IEvent : INotification
{
}

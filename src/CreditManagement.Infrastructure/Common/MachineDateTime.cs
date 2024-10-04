using CreditManagement.Application.Abstractions.Common;

namespace CreditManagement.Infrastructure.Common;

internal sealed class MachineDateTime : IDateTime
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}
using CreditManagement.Application.Abstractions.Common;
using CreditManagement.Application.Abstractions.Messaging;
using CreditManagement.Application.Core.Extensions;
using CreditManagement.Infrastructure.Common;
using CreditManagement.Infrastructure.Emails;
using CreditManagement.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;

namespace CreditManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //TODO email service and JWT issuer..etc can be configured here
        services.AddValidateOptions<EmailOptions>();
        services.AddValidateOptions<MessageBrokerOptions>();
        services.AddTransient<IDateTime, MachineDateTime>();
        services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();
        return services;
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CreditManagement.Application.Core.Extensions;

public static class ConfigurationExtensions
{
    public static TModel GetOptions<TModel>(this IConfiguration configuration, string section)
        where TModel : new()
    {
        var model = new TModel();
        configuration.GetSection(section).Bind(model);
        return model;
    }


    /// <summary>
    ///     Retrieves a configuration section by key. Throws an ArgumentNullException if the section does not exist.
    /// </summary>
    /// <param name="configuration">The IConfiguration instance.</param>
    /// <param name="sectionKey">The key of the configuration section.</param>
    /// <returns>The IConfiguration section.</returns>
    private static IConfiguration GetSection(
        IConfiguration configuration,
        string sectionKey
    )
    {
        var section = configuration.GetSection(sectionKey);
        if (string.IsNullOrWhiteSpace(section.Key))
            throw new ArgumentNullException($"Key {sectionKey} is missing in the configuration.");
        return section;
    }

    /// <summary>
    ///     Registers an options instance and configures it using data annotations.
    ///     This instance will be registered as a singleton.
    ///     This method won't throw exceptions immediately if configuration validation fails,
    ///     but will defer the error until the options are accessed.
    /// </summary>
    /// <typeparam name="TModel">The type of options to register.</typeparam>
    /// <param name="service">The IServiceCollection to add the services to.</param>
    public static void AddValidateOptions<TModel>(this IServiceCollection service)
        where TModel : class, new()
    {
        service.AddOptions<TModel>()
            .BindConfiguration(typeof(TModel).Name)
            .ValidateDataAnnotations();

        service.AddSingleton(x => x.GetRequiredService<IOptions<TModel>>().Value);
    }
}
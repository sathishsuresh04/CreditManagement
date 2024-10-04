using CreditManagement.Application;
using CreditManagement.Application.Core.Extensions;
using CreditManagement.Common.Exception;
using CreditManagement.Common.Swagger;
using CreditManagement.Common.Versioning;
using CreditManagement.Infrastructure;
using CreditManagement.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;

namespace CreditManagement;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseDefaultServiceProvider(
                (context, options) =>
                {
                    var isDevMode =
                        context.HostingEnvironment.IsDevelopment() ||
                        context.HostingEnvironment.IsEnvironment("test") ||
                        context.HostingEnvironment.IsStaging();

                    options.ValidateScopes = isDevMode;
                    options.ValidateOnBuild = isDevMode;
                });
            var openApiModel = builder.Configuration.GetOptions<OpenApiInfo>(nameof(OpenApiInfo));
            builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration)
                .AddPersistence(builder.Configuration);
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>()
                .AddProblemDetails()
                .Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true)
                .AddCustomSwagger(openApiModel, typeof(Program).Assembly)
                .AddCustomVersioning();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            app.UseExceptionHandler();
            var env = app.Environment;
            if (env.IsDevelopment()) await app.UseMigrationAsync(env);

            app.UseCustomSwagger();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                "default",
                "{controller=Account}/{action=Index}/{id?}");

            await app.RunAsync();
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Program terminated unexpectedly!");
            throw;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}
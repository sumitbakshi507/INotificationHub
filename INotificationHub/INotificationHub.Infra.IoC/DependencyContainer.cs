using INotificationHub.Domain.Core.Bus;
using INotificationHub.Infra.Bus;
using INotificationHub.NotificationHub.Data.Repository;
using INotificationHub.NotificationHub.Domain.EventHandlers;
using INotificationHub.NotificationHub.Domain.Events;
using INotificationHub.NotificationHub.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace INotificationHub.Infra.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.RollingFile("logs/log-{Date}.txt")
            .WriteTo.ColoredConsole()
            .CreateLogger();

            //Domain Bus
            services.AddSingleton<IEventBus, INotificationHubBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new INotificationHubBus(sp.GetService<IMediator>(), scopeFactory);
            });

            //Subscriptions
            services.AddTransient<NotificationCreatedEventHandler>();

            //Domain Events
            services.AddTransient<IEventHandler<NotificationCreatedEvent>, NotificationCreatedEventHandler>();

            //Domain Commands

            //Application Services

            //Data
            services.AddScoped<INotificationRepository, NotificationRepository>();

            Log.Information("Registration Completed");
        }
    }
}

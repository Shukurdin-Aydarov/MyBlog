using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SimpleEvenBus.Abstractions;
using SimpleEventBus.RabbitMQ;

using MyBlog.Location.Api.Infrastructure.Settings;

namespace MyBlog.Location.Api
{
    public static class EventBusStartup
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            ConfigureSettings(services);

            services.AddSingleton(CreateConnection);
            services.AddSingleton(CreateEventBus);

            return services;
        }

        private static void ConfigureSettings(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider()
                                        .GetRequiredService<IConfiguration>();

            services.Configure<EventBusSettings>(configuration.GetSection("EventBus"));
        }

        private static IPersistentConnection CreateConnection(IServiceProvider provider)
        {
            var logger = provider.GetRequiredService<ILogger<DefaultPersistentConnection>>();
            var options = provider.GetRequiredService<IOptions<EventBusSettings>>().Value;

            var factory = new ConnectionFactory
            {
                HostName = options.Host,
                DispatchConsumersAsync = true
            };

            if (!string.IsNullOrEmpty(options.UserName))
                factory.UserName = options.UserName;
            
            if (!string.IsNullOrEmpty(options.Password))
                factory.Password = options.Password;

            var retryCount = options.RetryCount ?? 5;

            return new DefaultPersistentConnection(factory, logger, retryCount);
        }

        private static IEventBus CreateEventBus(IServiceProvider provider)
        {
            var connection = provider.GetRequiredService<IPersistentConnection>();
            var logger = provider.GetRequiredService<ILogger<EventBus>>();
            var subscriptions = provider.GetRequiredService<ISubscriptionsManager>();
            var options = provider.GetRequiredService<IOptions<EventBusSettings>>().Value;
            var busOptions = new EventBusOptions
            {
                RetryCount = options.RetryCount ?? 5,
                QueueName = options.Queue,
                Exchange = options.Exchange
            };

            return new EventBus(connection, subscriptions, provider, busOptions, logger);
        }
    }
}

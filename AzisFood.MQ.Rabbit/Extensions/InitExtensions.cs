using System;
using AzisFood.MQ.Abstractions.Implementations;
using AzisFood.MQ.Abstractions.Interfaces;
using AzisFood.MQ.Rabbit.Implementations;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AzisFood.MQ.Rabbit.Extensions
{
    public static class InitExtensions
    {
        public static void AddRabbitMQSupport(this IServiceCollection serviceCollection, IConfiguration configuration,
            Action<IServiceCollectionBusConfigurator> configure = null)
        {
            // Add RabbitMQ MassTransit
            serviceCollection.AddTransient(typeof(IProducerService<>), typeof(ProducerService<>));
            serviceCollection.Configure<MQOptions>(configuration.GetSection(nameof(MQOptions)));
            serviceCollection.AddSingleton<IMQOptions>(sp =>
                sp.GetRequiredService<IOptions<MQOptions>>().Value);
            serviceCollection.AddMassTransit(config =>
            {
                configure?.Invoke(config);
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration.GetSection(nameof(MQOptions)).Get<MQOptions>().ConnectionString);
                    cfg.ConfigureEndpoints(ctx);
                });
            });
            serviceCollection.AddMassTransitHostedService();
        }
    }
}
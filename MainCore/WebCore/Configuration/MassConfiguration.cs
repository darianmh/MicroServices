using Infrastructure.Configuration;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebCore.Configuration
{
    public static class MassConfiguration
    {
        public static void UseMassTransit(this WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq(delegate (IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator)
                {

                    configurator.Host(RabbitConfiguration.Uri, "/", x =>
                    {
                        x.Username(RabbitConfiguration.Username);
                        x.Password(RabbitConfiguration.Password);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            });
            builder.Services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {
                    // if specified, waits until the bus is started before
                    // returning from IHostedService.StartAsync
                    // default is false
                    options.WaitUntilStarted = true;

                    // if specified, limits the wait time when starting the bus
                    options.StartTimeout = TimeSpan.FromSeconds(10);

                    // if specified, limits the wait time when stopping the bus
                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });
        }
    }
}

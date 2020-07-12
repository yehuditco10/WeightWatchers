using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using NServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Tracking.Services;

namespace TrackingHandler
{
    public class Program
    {
        static async Task Main()
        {
            Console.Title = "Tracking";

            var endpointConfiguration = new EndpointConfiguration("Tracking");
            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddSingleton<ITrackingService, TrackingService>();

            endpointConfiguration.EnableOutbox();
            //var connection = @"Data Source = DESKTOP-1HT6NS2; Initial Catalog = WeightWatchersOutBox; Integrated Security = True";
            var connection = @"Data Source = ILBHARTMANLT; Initial Catalog = WeightWatchersOutBox; Integrated Security = True";
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(connection);
                });

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString("host= localhost:5672;username=guest;password=guest");
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Delayed(
                customizations: delayed =>
                {
                    delayed.NumberOfRetries(2);
                    delayed.TimeIncrease(TimeSpan.FromMinutes(4));
                });

            recoverability.Immediate(
                customizations: immediate =>
                {
                    immediate.NumberOfRetries(1);

                });

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
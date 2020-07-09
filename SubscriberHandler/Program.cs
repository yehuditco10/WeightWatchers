using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WeightWatchers.Data;
using WeightWatchers.Services;
using WeightWatchers.Api;


namespace SubscriberHandler
{
    class Program
    {
        private readonly IConfiguration configuration;

        //public IConfiguration _Configuration { get; }
        public Program(IConfiguration configuration)
        {
            
            this.configuration = configuration;
        }
        static async Task Main()
        {
            Console.Title = "Subscriber";

            var endpointConfiguration = new EndpointConfiguration("Subscriber");
           
            endpointConfiguration.EnableOutbox();
            var connection = @"Data Source = DESKTOP-1HT6NS2; Initial Catalog = WeightWatchersOutBox; Integrated Security = True";
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

            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddSingleton<ISubscriberService, SubscriberService>();
            containerSettings.ServiceCollection.AddScoped<ISubscriberRepository, SubscriberRepository>();
            containerSettings.ServiceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //containerSettings.ServiceCollection.AddDbContext<WeightWatchersContext>(options =>
            //            options.UseSqlServer(
            //                configuration.GetConnectionString("weightWatchersConnection")));
            containerSettings.ServiceCollection.AddDbContext<WeightWatchersContext>(options =>
                        options.UseSqlServer(
                          "Data Source = DESKTOP-1HT6NS2; Initial Catalog = WeightWatchers; Integrated Security = True"));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            containerSettings.ServiceCollection.AddSingleton(mapper);

            ///?
            var endpointInstance = await NServiceBus.Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}

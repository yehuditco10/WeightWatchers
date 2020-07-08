using AutoMapper;
using Measure.Data;
using Measure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeightWatchers.Api;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using System.Configuration;
using System.Data.SqlClient;
using System;
using Messages;

namespace Measure.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<MeasureContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("MeasureDBConnection")));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            /////////////////////////////////////////////////////NSB//////////////////////////////
            //var endpointConfiguration = new EndpointConfiguration("Measure");

            ////persistence
            //var connection = Configuration.GetConnectionString("WeightWatchersOutBox");
            //var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            //var subscriptions = persistence.SubscriptionSettings();
            //subscriptions.CacheFor(TimeSpan.FromMinutes(1));
            //persistence.SqlDialect<SqlDialect.MsSqlServer>();
            //persistence.ConnectionBuilder(
            //    connectionBuilder: () =>
            //    {
            //        return new SqlConnection(connection);
            //    });
            //var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            //transport.UseConventionalRoutingTopology();
            //transport.ConnectionString("host= localhost:5672;username=guest;password=guest");
            //endpointConfiguration.EnableInstallers();
            //endpointConfiguration.EnableOutbox();
            //endpointConfiguration.AuditProcessedMessagesTo("audit");
            //var routing = transport.Routing();
            ////routing.RouteToEndpoint(
            ////assembly: typeof(UpdateCard).Assembly,
            ////destination: "WeightWatchers.Services");//?
            //routing.RouteToEndpoint(typeof(UpdateCard), "WeightWatchers.Services");
            //var endpointInstance = await Endpoint.Start(endpointConfiguration)
            // .ConfigureAwait(false);
            //var conventions = endpointConfiguration.Conventions();
            //conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
            //conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");
            //services.AddScoped(typeof(IEndpointInstance), x => endpointInstance);
            ////await endpointInstance.Stop()
            ////    .ConfigureAwait(false); 
            services.AddScoped(typeof(IMeasureService), typeof(MeasureService));
            services.AddScoped(typeof(IMeasureRepository), typeof(MeasureRepository));
           
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            //app.UseMiddleware(typeof(ErrorHandlerMiddlware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
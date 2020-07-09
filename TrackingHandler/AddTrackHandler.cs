using AutoMapper;
using Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tracking.Services;

namespace TrackingHandler
{
    public class AddTrackHandler : IHandleMessages<AddTrack>
    {
        private readonly ITrackingService _TrackingService;
        private readonly IMapper _Mapper;
        static ILog log = LogManager.GetLogger<AddTrackHandler>();
        public AddTrackHandler(ITrackingService trackingService,
            IMapper mapper)
        {
            _TrackingService = trackingService;
            _Mapper = mapper;
        }

        public Task Handle(AddTrack message, IMessageHandlerContext context)
        {
            log.Error("arrive to tracing");
            _TrackingService.AddTracking(_Mapper.Map<Tracking.Services.Models.Tracking>(message));
            return Task.CompletedTask;
        }
    }
}

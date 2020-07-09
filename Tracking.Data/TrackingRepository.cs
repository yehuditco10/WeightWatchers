using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tracking.Services;

namespace Tracking.Data
{
    public class TrackingRepository : ITrackingRepository
    {
        private readonly TrackingContext _TrackingContext;
        private readonly IMapper _Mapper;

        public TrackingRepository(TrackingContext trackingContext,
            IMapper mapper)
        {
            _TrackingContext = trackingContext;
            _Mapper = mapper;
        }
        public void AddTracking(Services.Models.Tracking message)
        {
            _TrackingContext.Trackings.Add(_Mapper.Map<Entities.Tracking>(message));
            _TrackingContext.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tracking.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly ITrackingRepository _TrackingRepository;

        public TrackingService(ITrackingRepository trackingRepository)
        {
            _TrackingRepository = trackingRepository;
                }
        public void AddTracking(Models.Tracking message)
        {
            _TrackingRepository.AddTracking(message);
        }
    }
}

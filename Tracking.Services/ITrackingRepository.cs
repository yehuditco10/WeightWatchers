﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tracking.Services
{
    public interface ITrackingRepository
    {
        void AddTracking(Models.Tracking message);
    }
}

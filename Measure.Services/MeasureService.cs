﻿using Measure.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Measure.Services
{
    public class MeasureService : IMeasureService
    {
        private readonly IMeasureRepository _measureRepository;

        public MeasureService(IMeasureRepository measureRepository)
        {
            _measureRepository = measureRepository;
        }
        public async Task<bool> CreateAsync(MeasureModel measure)
        {
            return await _measureRepository.CreateAsync(measure);
        }
    }
}

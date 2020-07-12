using AutoMapper;
using Measure.Services;
using Measure.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Measure.Data
{
    public class MeasureRepository : IMeasureRepository
    {

        public MeasureRepository(MeasureContext measureContext,
            IMapper imapper)
        {
            _measureContext = measureContext;
            _mapper = imapper;
        }

        private MeasureContext _measureContext { get; }

        private readonly IMapper _mapper;

        public async Task<int> CreateAsync(MeasureModel measureModel)
        {
            Entities.Measure measure = _mapper.Map<Entities.Measure>(measureModel);
            var e = await _measureContext.Measures.AddAsync(measure);

             await _measureContext.SaveChangesAsync();
            
            return e.Entity.id;
        }
        public async Task<int> UpdateStatus(int measureId, eStatus status)
        {
            Entities.Measure measure = new Entities.Measure()
            {
                id=measureId,
                status=status
            };
            _measureContext.Measures.Update(measure);
            return await _measureContext.SaveChangesAsync();
        }
    }
}

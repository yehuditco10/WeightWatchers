using Measure.Services.Models;
using Messages;
using NServiceBus;
using NServiceBus.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Measure.Services
{
    public class MeasureService : IMeasureService
    {
        private readonly IMeasureRepository _measureRepository;
       private readonly IEndpointInstance _endpointInstance;

        public MeasureService(IMeasureRepository measureRepository)
        {
            _measureRepository = measureRepository;

        }
        public MeasureService(IMeasureRepository measureRepository
           , IEndpointInstance endPointInstance
            )
        {
            _measureRepository = measureRepository;
          _endpointInstance = endPointInstance;
        }
        public async Task<bool> CreateAsync(MeasureModel measure)
        {
            measure.date = DateTime.Now;
            measure.status = eStatus.inProsses;
            var addedId= await _measureRepository.CreateAsync(measure);
            if(addedId != 0)
            {
                UpdateCard updateCard = new UpdateCard()
                {
                    cardId = measure.cardId,
                    measureId = addedId,
                    weight = measure.weight

                };
              
              await _endpointInstance.Send(updateCard);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateStatus(int measureId, bool isSucceeded)
        {
            eStatus status = isSucceeded == true ? eStatus.success : eStatus.failed;
            var sucsseded= await _measureRepository.UpdateStatus(measureId, status);
            if (sucsseded != 0)
                return true;
            return false;
        }

       
    }
}

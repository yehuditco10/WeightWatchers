using Messages;
using Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeightWatchers.Services;

namespace WeightWatchers.Api.NServiceBus
{
    public class UpdateCardHandler : IHandleMessages<UpdateCard>
    {
        private readonly ISubscriberService _subscriberService;
        static ILog log = LogManager.GetLogger<UpdateCardHandler>();
        public UpdateCardHandler(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }
        
        public async Task Handle(UpdateCard message, IMessageHandlerContext context)
        {
            log.Error($"Received UpdateCard in subscriber, weight = {message.weight} ...");
            bool succeded = false;
            var successedUpdate = await _subscriberService.UpdateCard(message.cardId, message.weight);
           // var successedUpdate = 1;
           
            if (successedUpdate != -1)
            {
                succeded = true;
                AddTrack addTrack = new AddTrack()
                {
                    CardId = message.cardId,
                    NewWeight = message.weight,
                };
                //await _endpointInstance.Send(addTrack);
              //  await context.Send(addTrack);
            }
            cardUpdated cardUpdated = new cardUpdated()
            {
                isSucceeded = succeded,
                measureId = message.measureId
            };
            //await _endpointInstance.Send(cardUpdated)
            await context.Publish(cardUpdated)
                       .ConfigureAwait(false);


        }
    }
}

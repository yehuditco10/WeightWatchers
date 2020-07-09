using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightWatchers.Data.Entities;
using WeightWatchers.Services;
using WeightWatchers.Services.Models;

namespace WeightWatchers.Data
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly WeightWatchersContext _context;
        private readonly IMapper _mapper;

        public SubscriberRepository(WeightWatchersContext watchersContext,
            IMapper mapper)
        {
            _context = watchersContext;
            _mapper = mapper;
        }

        public IMapper Mapper { get; }

        public async Task<int> AddAsync(SubscriberModel subsciberModel, float height)
        {
            try
            {
                subsciberModel.id = Guid.NewGuid();
                Subscriber s = _mapper.Map<Subscriber>(subsciberModel);
                await _context.Subscribers.AddAsync(s);
                await _context.Cards.AddAsync(new Card()
                {
                    BMI = 0,
                    subscriberId = subsciberModel.id,
                    height = height,
                    openDate = DateTime.Today

                });
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CardModel> GetByIdAsync(int cardId)
        {
            try
            {
                Card card = await _context.Cards.FirstOrDefaultAsync(c => c.id == cardId);
                if (card == null)
                {
                    throw new Exception("The id isn't exists");
                }
                var moreDetails = await _context.Subscribers.FirstOrDefaultAsync(s => s.id.Equals(card.subscriberId));
                //card.subscriber.firstName = moreDetails.firstName;
                //card.subscriber.lastName = moreDetails.lastName;
                return _mapper.Map<CardModel>(card);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }


        }

        public async Task<CardModel> isCardExists(int cardId)
        {
           
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.id == cardId);
            Console.WriteLine("bbbb");
            return _mapper.Map<CardModel>(card);

        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            Subscriber subscriber = await _context.Subscribers.FirstOrDefaultAsync(
                s => s.email == email);
            if (subscriber == null)
            {
                return false;
            }
            return true;
        }

        public async Task<int> LoginAsync(string email, string password)
        {
            Subscriber subscriber = await _context.Subscribers.FirstOrDefaultAsync(
                s => s.email == email && s.password == password);
            if (subscriber == null)
            {
                return -1;
            }
            var card = await _context.Cards.FirstOrDefaultAsync(
                c => c.subscriberId == subscriber.id);
            return card.id;
        }

        //Has to be async???
        public async Task<int> UpdateCard(CardModel cardUpdated)
        {
            Card card = _mapper.Map<Entities.Card>(cardUpdated);
            Card cartToUpdate = await _context.Cards.FirstOrDefaultAsync(c => c.id == cardUpdated.id);
            cartToUpdate.weight = cardUpdated.weight;
            cartToUpdate.BMI = cardUpdated.BMI;
            cartToUpdate.updateDate = cardUpdated.updateDate;
            //_context.Cards.Update(card);
            return await _context.SaveChangesAsync();
        }


    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeightWatchers.Services.Models;

namespace WeightWatchers.Services
{
   public interface ISubscriberService
    {
        Task<CardModel> GetByIdAsync(int cardId);
        Task<int> LoginAsync(string email, string password);
        Task<bool> AddAsync(SubscriberModel subsciber, float height);
    }
}

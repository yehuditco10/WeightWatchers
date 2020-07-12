using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WeightWatchers.Api.DTO;
using WeightWatchers.Data;
using WeightWatchers.Data.Entities;
using WeightWatchers.Services;
using WeightWatchers.Services.Models;

namespace WeightWatchers.Api
{
   public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<SubscriberDTO, SubscriberModel>();
            CreateMap<CardDTO, CardModel>();
            CreateMap<SubscriberModel, Subscriber>();
            CreateMap<CardModel, Card>();
            CreateMap<Card, CardModel>().ForMember(name => name.firstName, to => to.MapFrom(n => n.subscriber.firstName))
                .ForMember(name => name.lastName, to => to.MapFrom(n => n.subscriber.lastName));;
            CreateMap<CardModel, CardDTO>();//.ForMember(name=>name.firstName,to=>to.MapFrom(n=>n.firstName))
             //   .ForMember(name => name.lastName, to => to.MapFrom(n => n.lastName));
        }
    }
}

using AutoMapper;
using Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracking.Api
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<AddTrack,Services.Models.Tracking>();
            CreateMap<Services.Models.Tracking,Data.Entities.Tracking>();
        }
    }
}


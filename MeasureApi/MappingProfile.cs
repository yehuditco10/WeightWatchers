using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using MeasureApi.DTO;
using Measure.Data;
using Measure.Data.Entities;
using Measure.Services;
using Measure.Services.Models;

namespace MeasureApi
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MeasureApi.DTO.Measure, MeasureModel>();
            CreateMap<MeasureModel, Measure.Data.Entities.Measure>();
        }
    }
}

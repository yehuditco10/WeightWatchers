using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Measure.Services;
using Measure.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeasureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMeasureService _measureService;


        public MeasureController(IMapper mapper,
            IMeasureService measureService)
        {
            _mapper = mapper;
            _measureService = measureService;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateAsync(DTO.Measure measureDTO)
        {
            var measure = _mapper.Map<MeasureModel>(measureDTO);
            return await _measureService.CreateAsync(measure);
        }
    }
}
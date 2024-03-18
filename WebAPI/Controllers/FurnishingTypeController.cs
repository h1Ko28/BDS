using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    public class FurnishingTypeController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public FurnishingTypeController(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllFurnishingType() {
            var furnishingType = await uow.FurnishingTypeRepository.GetFurnishingTypes();
            var furnishingTypeDto = mapper.Map<IEnumerable<KeyPairDto>>(furnishingType);
            return Ok(furnishingTypeDto);
        }
    }
}
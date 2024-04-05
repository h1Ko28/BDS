using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public PropertyController(IUnitOfWork uow, IMapper mapper,
                                    IPhotoService photoService)
        {
            this.photoService = photoService;
            this.mapper = mapper;
            this.uow = uow;
        }
        
        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProperties(int sellRent) {
            var properties = await uow.PropertyRepository.GetPropertiesAsync(sellRent);
            var propertyListDto = mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertyListDto);
        }

        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id) {
            var property = await uow.PropertyRepository.GetPropertyDetailAsync(id);
            var propertyDto = mapper.Map<PropertyDetailDto>(property);
            return Ok(propertyDto);
        }
        
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddProperty(PropertyDto propertyDto) {
            var property = mapper.Map<Property>(propertyDto);
            var userId = GetUserId();
            property.PostedBy = userId;
            property.LastUpdatedBy = userId;
            uow.PropertyRepository.AddProperty(property);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpPost("add/photo/{id}")]
        [Authorize]
        public async Task<IActionResult> UploadImage(IFormFile photo, int id) {
            var upload = await photoService.AddPhotoAsync(photo);
            if (upload.Error != null)
                return BadRequest(upload);

            var property = await uow.PropertyRepository.GetPropertyDetailAsync(id);
            var photos = new Photo{
                ImageUrl = upload.SecureUrl.AbsoluteUri,
                PublicId = upload.PublicId
            };

            if(property.Photos.Count == 0)
                photos.IsPrimary = true;

            property.Photos.Add(photos);
            await uow.SaveAsync();
            return Ok(201);
        }
    }
}
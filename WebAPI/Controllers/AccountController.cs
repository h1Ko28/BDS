using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WebAPI.Dto;
using WebAPI.Dtos;
using WebAPI.Errors;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IConfiguration configuration;
        public AccountController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            ApiError error = new ApiError();

            if (loginReq.UserName.isEmpty() || loginReq.Password.isEmpty()) {
                error.ErrorCode = BadRequest().StatusCode;
                error.ErrorMessage = "Username or password can not be blank";
                return BadRequest(error);
            }
            var user = await unitOfWork.UserRepository.Authenticate(loginReq.UserName, loginReq.Password);

            if (user == null) {
                return Unauthorized("Invalid username or password");
            }

            var loginRes = new LoginResDto();
            loginRes.UserName = user.Username;
            loginRes.Token = CreateJWT(user);
            return Ok(loginRes);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            if (await unitOfWork.UserRepository.UserAlreadyExists(loginReq.UserName))
                return BadRequest("UserName already exists");
            
            unitOfWork.UserRepository.Register(loginReq.UserName, loginReq.Password);
            
            await unitOfWork.SaveAsync();
            return StatusCode(201); 
        }

        private string CreateJWT(User user) 
        {
            var secretKey = configuration.GetSection("AppSettings:key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(secretKey));    
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
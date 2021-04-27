using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repository.Service.Interface;
using WebApi.Repository.ViewModels;

namespace WebApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration; 
        public AccountController(IUnitOfWorkService unitOfWork, IMapper mapper,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _unitOfWork.UserRepository.FindUserById(id);
            return Ok(user); 
        }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult>Loggin(UserViewModel userVM)
        {
            var user = await _unitOfWork.UserRepository.Authenticate(userVM.UserName, userVM.Password);
            if(user == null)
            {
                return Unauthorized("Invalid User Id or Password");
            }
            var loginResponse = new LoginResultViewModel();
            loginResponse.UserName = user.UserName;
            loginResponse.Role = user.Role;
            loginResponse.Token = CreateJwt(user);
            return Ok(loginResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserViewModel userVM)
        {
            var isExistUser = await _unitOfWork.UserRepository.UserAlreadyExists(userVM.UserName);
            if (isExistUser)
            {
                return BadRequest("User already exists, please try something else");
            }
            _unitOfWork.UserRepository.Register(userVM.UserName, userVM.Password, userVM.Role);
            await _unitOfWork.SaveChangeAsync();
            return StatusCode(201);
        }
        //======Genarate Token by JWT
        private string CreateJwt(User user)
        {
            var secretKey = _configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            var signignCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signignCredentials
            };
            //token handlar : That will be responsible for genarete JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        [AllowAnonymous]
        [HttpGet("getUserName/{userName}")]
        public async Task<IActionResult> GetAllUser(string userName)
        {
            var user = await _unitOfWork.UserRepository.GetAllUserAsync();
            return Ok(user);
        }
    }
}

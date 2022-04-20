using BLL.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace MovieTheaterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfigurationRoot _configuration;

        public UserController(UserService userService,
            IConfigurationRoot configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }


        [HttpGet]
        [Route("users/{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("{email}")]
        public IActionResult GetByEmail(string email)
        {
            var user = _userService.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("login")]
        public IActionResult LogIn(UserLoginDTO user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var foundUser = _userService.LogIn(user);

            if (foundUser == null)
            {
                return Unauthorized();
            }

            var tokenString = GenerateJsonWebToken(foundUser);

            return Ok(new {user = foundUser, token = tokenString });
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp(UserRegisterDTO user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            await _userService.SignUp(user);
            return Ok();
        }

        private string GenerateJsonWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["Key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                _configuration.GetSection("Jwt")["Issuer"],
                _configuration.GetSection("Jwt")["Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

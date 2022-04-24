using BLL.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieTheaterApi.JWT;
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
        private readonly UserManager<User> _userManager;
        private readonly JwtHandler _jwtHandler;
        private readonly IConfigurationRoot _configuration;

        public UserController(UserService userService,
            UserManager<User> userManager,
            JwtHandler jwtHandler,
            IConfigurationRoot configuration)
        {
            _userService = userService;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
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

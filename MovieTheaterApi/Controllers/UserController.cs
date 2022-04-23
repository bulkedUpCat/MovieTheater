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

        // POST api/<UserController>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn(UserLoginDTO user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var foundUser = await _userService.LogIn(user);

            if (foundUser == null)
            {
                return Unauthorized();
            }

            var claims = _jwtHandler.GetClaims(foundUser);
            var userRoles = await _userManager.GetRolesAsync(foundUser);

            foreach(var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var signingCredentials = _jwtHandler.GetSigningCredentials();

            var token = _jwtHandler.GenerateToken(signingCredentials, claims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp(UserRegisterDTO user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var newUser = await _userService.SignUp(user);
            
            if (newUser == null)
            {
                return BadRequest();
            }

            return Ok(newUser);
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

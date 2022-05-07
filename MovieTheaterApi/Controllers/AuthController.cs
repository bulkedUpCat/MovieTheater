using BLL.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieTheaterApi.JWT;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MovieTheaterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly JwtHandler _jwtHandler;
        private readonly IConfigurationRoot _configuration;

        public AuthController(UserService userService,
            UserManager<User> userManager,
            JwtHandler jwtHandler,
            IConfigurationRoot configuration)
        {
            _userService = userService;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn(UserLoginDTO user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var foundUser = await _userService.LogInAsync(user);

            if (foundUser == null)
            {
                return Unauthorized();
            }

            var claims = _jwtHandler.GetClaims(foundUser);
            var userRoles = await _userManager.GetRolesAsync(foundUser);

            foreach (var role in userRoles)
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

            var newUser = await _userService.SignUpAsync(user);

            if (newUser == null)
            {
                return BadRequest();
            }

            return Ok(newUser);
        }


        [HttpGet("ConfirmEmailLink")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var result = await _userService.ConfirmEmailAsync(token, email);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("https://localhost:4200/login");
        }
    }
}

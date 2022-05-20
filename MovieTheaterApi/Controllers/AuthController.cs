using BLL.Abstractions.Interfaces;
using BLL.Email;
using BLL.Services;
using BLL.Validators;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly JwtHandler _jwtHandler;
        private readonly EmailSender _emailSender;
        private readonly IConfigurationRoot _configuration;

        public AuthController(IUserService userService,
            UserManager<User> userManager,
            JwtHandler jwtHandler,
            EmailSender emailSender,
            IConfigurationRoot configuration)
        {
            _userService = userService;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn(UserLoginDTO user)
        {
            if (user == null)
            {
                return BadRequest("No credentials were provided");
            }

            var foundUser = await _userService.LogInAsync(user);

            if (foundUser == null)
            {
                return Unauthorized("Wrong email or password");
            }

            var claims = await _jwtHandler.GetClaims(foundUser);

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

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", model.Email }
            };
    
            var callback = QueryHelpers.AddQueryString(model.ClientURI, param);

            try
            {
                _emailSender.SendSmtpMail(new EmailTemplate()
                {
                    To = user.Email,
                    Subject = "Password reset",
                    Body = $"Click this link to reset your password:\n{callback}",
                    UserId = user.Id
                });
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPost("passwordReset")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordDTO model)
        {
            if (!ModelState.IsValid || model.Password != model.ConfirmPassword)
            {
                return BadRequest("Model state not valid");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (string.IsNullOrEmpty(model.Token))
            {
                return BadRequest("Empty password reset token");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest("Failed to reset the password");
            }

            return Ok();
        }
    }
}

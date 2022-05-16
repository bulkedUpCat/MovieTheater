using BLL.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public UserController(UserService userService,
            UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetALl()
        {
            var users = await _userManager.Users
                .Include(u => u.Comments)
                .ToListAsync();

            return users;
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
            var user = _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPost("changeUsername")]
        public async Task<IActionResult> ChangeUsername(ChangeUsernameDTO changeUsername)
        {
            var result = await _userService.ChangeUsernameAsync(changeUsername);

            return result ? Ok() : BadRequest();
        }


        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePassword)
        {
            var result = await _userService.ChangePasswordAsync(changePassword);

            return result ? Ok() : BadRequest();
        }

        [HttpPost("changeEmail")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailDTO changeEmail)
        {
            var result = await _userService.ChangeEmailAsync(changeEmail);

            return result ? Ok() : BadRequest();
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

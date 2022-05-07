
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MovieTheaterApi.Controllers
{
    [Route("api/roles")]
    [ApiController]
   // [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var user = await _userManager.FindByNameAsync("admin18");
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            return Ok(_userManager.GetRolesAsync(user));
        }
    }
}

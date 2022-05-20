using BLL.Abstractions.Interfaces;
using BLL.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieTheaterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchLaterController : ControllerBase
    {
        private readonly IWatchLaterListService _userListService;

        public WatchLaterController(IWatchLaterListService userListsService)
        {
            _userListService = userListsService;   
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Movie>> GetWatchLaterMoviesOfUser(string id)
        {
            return await _userListService.GetWatchLaterMoviesOfUserAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWatchLaterList(MovieUser movieUser)
        {

            var result = await _userListService.AddToWatchLaterListAsync(movieUser);

            return result == false ? BadRequest() : Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromWatchLaterList(MovieUser movieUser)
        {
            var result = await _userListService.RemoveFromWatchLaterListAsync(movieUser);

            return result == false ? BadRequest() : Ok();
        }
    }
}

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
        private readonly WatchLaterListService _userListService;

        public WatchLaterController(WatchLaterListService userListsService)
        {
            _userListService = userListsService;   
        }

        [HttpGet("{id}")]
        public IEnumerable<Movie> GetWatchLaterMoviesOfUser(int id)
        {
            return _userListService.GetWatchLaterMoviesOfUser(id);
        }

        [HttpPost]
        public IActionResult AddToWatchLaterList(MovieUser movieUser)
        {

            var result = _userListService.AddToWatchLaterList(movieUser);

            return result == false ? BadRequest() : Ok();
        }

        [HttpDelete]
        public IActionResult RemoveFromWatchLaterList(MovieUser movieUser)
        {
            var result = _userListService.RemoveFromWatchLaterList(movieUser);

            return result == false ? BadRequest() : Ok();
        }
    }
}

using BLL.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieTheaterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteListController : ControllerBase
    {
        private readonly FavoriteListService _favoriteListService;

        public FavoriteListController(FavoriteListService favoriteListService)
        {
            _favoriteListService = favoriteListService;
        }

        [HttpGet("{id}")]
        public IEnumerable<Movie> GetFavoriteMoviesOfUser(string id)
        {
            return _favoriteListService.GetFavoriteMoviesOfUser(id);
        }

        [HttpPost]
        public IActionResult AddToFavoriteList(MovieUser movieUser)
        {
            var result = _favoriteListService.AddToFavoriteList(movieUser);

            return result == false ? BadRequest() : Ok();
        }

        [HttpDelete]
        public IActionResult RemoveFromFavoriteList(MovieUser movieUser)
        {
            var result = _favoriteListService.RemoveFromFavoriteList(movieUser);

            return result == false ? BadRequest() : Ok();
        }
    }
}

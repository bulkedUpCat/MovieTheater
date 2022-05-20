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
    public class FavoriteListController : ControllerBase
    {
        private readonly IFavoriteListService _favoriteListService;

        public FavoriteListController(IFavoriteListService favoriteListService)
        {
            _favoriteListService = favoriteListService;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Movie>> GetFavoriteMoviesOfUser(string id)
        {
            return await _favoriteListService.GetFavoriteMoviesOfUserAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavoriteList(MovieUser movieUser)
        {
            var result = await _favoriteListService.AddToFavoriteListAsync(movieUser);

            return result == false ? BadRequest() : Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromFavoriteList(MovieUser movieUser)
        {
            var result = await _favoriteListService.RemoveFromFavoriteListAsync(movieUser);

            return result == false ? BadRequest() : Ok();
        }
    }
}

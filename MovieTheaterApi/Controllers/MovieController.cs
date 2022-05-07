using BLL.Services;
using Core.DTOs;
using Core.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MovieTheaterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;
        private readonly UserService _userService;

        public MovieController(MovieService movieService,
            UserService userService)
        {
            _movieService = movieService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies([FromQuery] MovieParameters movieParameters)
        {
            var movies = await _movieService.GetPagedMoviesAsync(movieParameters);

            var data = new
            {
                movies.TotalCount,
                movies.PageSize,
                movies.CurrentPage,
                movies.HasNext,
                movies.HasPrevious
            };

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<Movie> Get(int id)
        {
            return await _movieService.GetMovieById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MovieDTO movieDTO)
        {
            var result = await _movieService.AddMovieAsync(movieDTO);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

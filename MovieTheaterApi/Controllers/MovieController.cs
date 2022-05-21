using BLL.Abstractions.Interfaces;
using BLL.Services;
using BLL.Validators;
using Core.DTOs;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MovieTheaterApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieService movieService,
            ILogger<MovieController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies([FromQuery] MovieParameters movieParameters)
        {
            var movies = await _movieService.GetPagedMoviesAsync(movieParameters);

            if (movies == null)
            {
                _logger.LogWarning("Failed to find any movies");
                return NotFound("No movie found");
            }

            _logger.LogInformation("Getting all the movies");
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            Movie movie;

            try
            {
                movie = await _movieService.GetMovieById(id);
            }
            catch (MovieException e)
            {
                _logger.LogWarning($"Failed to find a movie with id {id}");
                return BadRequest(e.Message);
            }

            _logger.LogInformation($"Getting a movie with id {id}");
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddMovieDTO movieDTO)
        {
            try
            {
                var movie = await _movieService.AddMovieAsync(movieDTO);

                _logger.LogInformation("Movie was created");
                return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
            }
            catch (MovieException e)
            {
                _logger.LogWarning("Failed to create a movie");
                return BadRequest(e.Message);
            }
        }

        [HttpPost("report")]
        public async Task<IActionResult> CreateReport(MovieParameters movieParameters)
        {
            var result = await _movieService.CreateReport(movieParameters);

            if (!result)
            {
                _logger.LogWarning("Failed to create a report");
                return BadRequest("Failed to create a movie report");
            }

            _logger.LogInformation("Report was created");
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _movieService.DeleteMovieAsync(id);
            }
            catch (MovieException e)
            {
                _logger.LogWarning($"Failed to delete the movie with id {id}");
                return BadRequest(e.Message);
            }

            _logger.LogInformation($"Movie with id {id} was deleted");
            return Ok();
        }
    }
}

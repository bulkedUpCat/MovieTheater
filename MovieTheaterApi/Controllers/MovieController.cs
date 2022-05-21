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

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies([FromQuery] MovieParameters movieParameters)
        {
            var movies = await _movieService.GetPagedMoviesAsync(movieParameters);

            if (movies == null)
            {
                return NotFound("No movie found");
            }

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
                return BadRequest(e.Message);
            }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddMovieDTO movieDTO)
        {
            try
            {
                var movie = await _movieService.AddMovieAsync(movieDTO);

                return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("report")]
        public async Task<IActionResult> CreateReport(MovieParameters movieParameters)
        {
            var result = await _movieService.CreateReport(movieParameters);

            if (!result)
            {
                return BadRequest("Failed to create a movie report");
            }

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
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}

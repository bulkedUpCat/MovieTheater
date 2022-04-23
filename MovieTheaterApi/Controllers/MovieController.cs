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

        // GET: api/<MovieController>
        [HttpGet]
        public IEnumerable<Movie> GetAllMovies()
        {
           
            return _movieService.GetAllMovies();
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            return _movieService.FirstOrDefault(m => m.Id == id);
        }

        // POST api/<MovieController>
        [HttpPost]
        public async Task Post(MovieDTO movieDTO)
        {
            /*var movie = _movieService.FirstOrDefault(
                m => m.Title == movieDTO.Title &&
                m.Genre == movieDTO.Genre &&
                m.YearReleased == movieDTO.YearReleased);

           await _movieService.AddMovie(movie);*/
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

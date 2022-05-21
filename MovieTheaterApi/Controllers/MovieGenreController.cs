﻿using BLL.Abstractions.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieTheaterApi.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class MovieGenreController : ControllerBase
    {
        private readonly IMovieGenreService _genreService;

        public MovieGenreController(IMovieGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var genres = await _genreService.GetAllGenres();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            return Ok(genre);
        }
    }
}

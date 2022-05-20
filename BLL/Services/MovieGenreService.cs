using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MovieGenreService : IMovieGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieGenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MovieGenre>> GetAllGenres()
        {
            var genres = await _unitOfWork.MovieGenreRepository.GetAsync();
            return genres;
        }

        public async Task<MovieGenre> GetGenreByIdAsync(int id)
        {
            var genre = await _unitOfWork.MovieGenreRepository.GetByIdAsync(id);
            return genre;
        }
    }
}

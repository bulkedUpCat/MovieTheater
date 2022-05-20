using BLL.Abstractions.Interfaces;
using Core.DTOs;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FavoriteListService : IFavoriteListService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Movie>> GetFavoriteMoviesOfUserAsync(string id)
        {
            var user = (await _unitOfWork.UserRepository.GetAsync(u => u.Id == id, null, "FavoriteListMovies"))
                .FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            return user.FavoriteListMovies;
        }

        public async Task<bool> AddToFavoriteListAsync(MovieUser movieUser)
        {
            if (movieUser == null)
            {
                return false;
            }

            var user = (await _unitOfWork.UserRepository.GetAsync(u => u.Id == movieUser.UserId, null, "FavoriteListMovies"))
                .FirstOrDefault();
            var movie = await _unitOfWork.MovieRepository.GetByIdAsync(movieUser.MovieId);

            if (user == null ||
                movie == null ||
                user.FavoriteListMovies.Contains(movie))
            {
                return false;
            }

            try
            {
                user.FavoriteListMovies.Add(movie);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RemoveFromFavoriteListAsync(MovieUser movieUser)
        {
            if (movieUser == null)
            {
                return false;
            }

            var user = (await _unitOfWork.UserRepository.GetAsync(u => u.Id == movieUser.UserId, null, "FavoriteListMovies"))
                .FirstOrDefault();
            var movie = await _unitOfWork.MovieRepository.GetByIdAsync(movieUser.MovieId);

            if (user == null ||
                movie == null ||
                !user.FavoriteListMovies.Contains(movie))
            {
                return false;
            }

            try
            {
                user.FavoriteListMovies.Remove(movie);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}

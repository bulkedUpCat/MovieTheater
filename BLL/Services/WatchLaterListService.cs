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
    public class WatchLaterListService : IWatchLaterListService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WatchLaterListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Movie>> GetWatchLaterMoviesOfUserAsync(string id)
        {
            var user = (await _unitOfWork.UserRepository.GetAsync(u => u.Id == id, null, "WatchLaterMovies"))
                .FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            return user.WatchLaterMovies;
        }

        public async Task<bool> AddToWatchLaterListAsync(MovieUser movieUser)
        {
            if (movieUser == null)
            {
                return false;
            }

            var user = (await _unitOfWork.UserRepository.GetAsync(u => u.Id == movieUser.UserId, null, "WatchLaterMovies"))
                .FirstOrDefault();
            var movie = await _unitOfWork.MovieRepository.GetByIdAsync(movieUser.MovieId);

            if (user == null ||
                movie == null ||
                user.WatchLaterMovies.Contains(movie))
            {
                return false;
            }

            try
            {
                user.WatchLaterMovies.Add(movie);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RemoveFromWatchLaterListAsync(MovieUser movieUser)
        {
            if (movieUser == null)
            {
                return false;
            }

            var user = (await _unitOfWork.UserRepository.GetAsync(u => u.Id == movieUser.UserId, null, "WatchLaterMovies"))
                .FirstOrDefault();
            var movie = await  _unitOfWork.MovieRepository.GetByIdAsync(movieUser.MovieId);

            if (user == null ||
                movie == null ||
                !user.WatchLaterMovies.Contains(movie))
            {
                return false;
            }

            try
            {
                user.WatchLaterMovies.Remove(movie);
                await _unitOfWork.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}

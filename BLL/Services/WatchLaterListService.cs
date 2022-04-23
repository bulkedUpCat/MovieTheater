using Core.DTOs;
using Core.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class WatchLaterListService
    {
        private readonly UnitOfWork _unitOfWork;

        public WatchLaterListService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Movie> GetWatchLaterMoviesOfUser(string id)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.Id == id, null, "WatchLaterMovies").FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            return user.WatchLaterMovies;
        }

        public bool AddToWatchLaterList(MovieUser movieUser)
        {
            if (movieUser == null)
            {
                return false;
            }

            var user = _unitOfWork.UserRepository.Get(u => u.Id == movieUser.UserId, null, "WatchLaterMovies")
                .FirstOrDefault();
            var movie = _unitOfWork.MovieRepository.GetByID(movieUser.MovieId);

            if (user == null ||
                movie == null ||
                user.WatchLaterMovies.Contains(movie))
            {
                return false;
            }

            try
            {
                user.WatchLaterMovies.Add(movie);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool RemoveFromWatchLaterList(MovieUser movieUser)
        {
            if (movieUser == null)
            {
                return false;
            }

            var user = _unitOfWork.UserRepository.Get(u => u.Id == movieUser.UserId, null, "WatchLaterMovies")
                .FirstOrDefault();
            var movie = _unitOfWork.MovieRepository.GetByID(movieUser.MovieId);

            if (user == null ||
                movie == null ||
                !user.WatchLaterMovies.Contains(movie))
            {
                return false;
            }

            try
            {
                user.WatchLaterMovies.Remove(movie);
                _unitOfWork.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}

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
    public class FavoriteListService
    {
        private readonly UnitOfWork _unitOfWork;

        public FavoriteListService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Movie> GetFavoriteMoviesOfUser(string id)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.Id == id, null, "FavoriteListMovies")
                .FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            return user.FavoriteListMovies;
        }

        public bool AddToFavoriteList(MovieUser movieUser)
        {
            if (movieUser == null)
            {
                return false;
            }

            var user = _unitOfWork.UserRepository.Get(u => u.Id == movieUser.UserId, null, "FavoriteListMovies")
                .FirstOrDefault();
            var movie = _unitOfWork.MovieRepository.GetByID(movieUser.MovieId);

            if (user == null ||
                movie == null ||
                user.FavoriteListMovies.Contains(movie))
            {
                return false;
            }

            try
            {
                user.FavoriteListMovies.Add(movie);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool RemoveFromFavoriteList(MovieUser movieUser)
        {
            if (movieUser == null)
            {
                return false;
            }

            var user = _unitOfWork.UserRepository.Get(u => u.Id == movieUser.UserId, null, "FavoriteListMovies")
                .FirstOrDefault();
            var movie = _unitOfWork.MovieRepository.GetByID(movieUser.MovieId);

            if (user == null ||
                movie == null ||
                !user.FavoriteListMovies.Contains(movie))
            {
                return false;
            }

            try
            {
                user.FavoriteListMovies.Remove(movie);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}

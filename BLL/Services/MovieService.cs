using BLL.Abstractions.Interfaces;
using Core.DTOs;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MovieService //: IMovieService
    {
        private readonly UnitOfWork _unitOfWork;

        public MovieService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _unitOfWork.MovieRepository.Get();
        }

        public bool AddMovie(Movie movie)
        {
            if (movie == null)
            {
                return false;
            }

            try
            {
                _unitOfWork.MovieRepository.Insert(movie);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            } 

            return true;
        }

        public bool DeleteMovie(Movie movie)
        {
            if (movie == null)
            {
                return false;
            }

            try
            {
                _unitOfWork.MovieRepository.Delete(movie);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            
            return true;
        }

        public bool UpdateAsync (Movie updatedMovie)
        {
            if (updatedMovie == null)
            {
                return false;
            }

            try
            {
                _unitOfWork.MovieRepository.Update(updatedMovie);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Movie> Where(Expression<Func<Movie, bool>> expression)
        {
            return _unitOfWork.MovieRepository.Where(expression);
        }

        public Movie FirstOrDefault(Expression<Func<Movie, bool>> expression)
        {
            return _unitOfWork.MovieRepository.FirstOrDefault(expression);
        }

        public bool Any(Expression<Func<Movie, bool>> expression)
        {
            return _unitOfWork.MovieRepository.Any(expression);
        }
    }
}

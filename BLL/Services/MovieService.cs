using AutoMapper;
using BLL.Abstractions.Interfaces;
using Core.DTOs;
using Core.Helpers;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            var movies = await _unitOfWork.MovieRepository.GetAsync();
            return movies;
        }

        public async Task<PagedList<Movie>> GetPagedMoviesAsync(MovieParameters movieParameters)
        {
            var movies = await _unitOfWork.MovieRepository.GetPagedMovies(movieParameters);
            if (movies == null) throw new Exception("Movies not found!");

            List<Movie> newMovies = new List<Movie>();
            int count;

            foreach (var movie in movies)
            {
                count = 0;

                foreach (var parameter in movieParameters.Genres) 
                {
                    foreach (var genre in movie.Genres)
                    {
                        if (genre.Name == parameter) count++ ;
                        
                    }
                }

                if (count >= movieParameters.Genres.Count)
                {
                    newMovies.Add(movie);
                }
            }

            var pagedList = PagedList<Movie>.ToPagedList(newMovies, movieParameters.PageNumber, movieParameters.PageSize);


            return pagedList;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _unitOfWork.MovieRepository.GetByIdAsync(id);
            return movie;
        }

        public async Task<bool> AddMovieAsync(MovieDTO movieDTO)
        {
            if (movieDTO == null)
            {
                return false;
            }

            var movie = new Movie()
            {
                Title = movieDTO.Title,
                Description = movieDTO.Description,
                ReleaseDate = movieDTO.Year,
                Director = movieDTO.Director,
                RuntimeHours = movieDTO.Runtime,
                Image = $"{string.Join("",movieDTO.Title.Split(" "))}.jpg"
            };

            var genres = await _unitOfWork.MovieGenreRepository.GetAsync(mg => movieDTO.Genres.Contains(mg.Name));

            movie.Genres = (ICollection<MovieGenre>) genres;

            try
            {
                await _unitOfWork.MovieRepository.InsertAsync(movie);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            } 

            return true;
        }

        public async Task<bool> DeleteMovie(Movie movie)
        {
            if (movie == null)
            {
                return false;
            }

            try
            {
                _unitOfWork.MovieRepository.Delete(movie);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            
            return true;
        }

        public async Task<bool> UpdateAsync (Movie updatedMovie)
        {
            if (updatedMovie == null)
            {
                return false;
            }

            try
            {
                _unitOfWork.MovieRepository.Update(updatedMovie);
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

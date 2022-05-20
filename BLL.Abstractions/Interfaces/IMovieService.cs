using Core.DTOs;
using Core.Helpers;
using Core.Models;

namespace BLL.Abstractions.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<PagedList<MovieDTO>> GetPagedMoviesAsync(MovieParameters movieParameters);
        Task<Movie> GetMovieById(int id);
        Task<bool> AddMovieAsync(AddMovieDTO movieDTO);
        Task<bool> DeleteMovieAsync(int id);
        Task<bool> UpdateMovieAsync(Movie updatedMovie);
        Task<bool> CreateReport(MovieParameters movieParameters);
    }
}

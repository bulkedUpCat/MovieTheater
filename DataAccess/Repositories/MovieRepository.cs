using Core.Helpers;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAsync(Expression<Func<Movie, bool>> filter = null, Func<IQueryable<Movie>, IOrderedQueryable<Movie>> orderBy = null, string includeProperties = "")
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }

        public async Task<Movie> GetByIdAsync(object id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == (int)id);
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetPagedMovies(MovieParameters movieParameters)
        {
            var movies = await _context.Movies
                .Where(m => movieParameters.Years.Contains(m.ReleaseDate.ToString()) || movieParameters.Years.Count == 0)
                .Include(m => m.Genres)
                .ToListAsync();

            return movies;
        }

        public async Task InsertAsync(Movie entity)
        {
            await _context.Movies.AddAsync(entity);
        }

        public void Update(Movie entityToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Movie entityToDelete)
        {
            throw new NotImplementedException();
        }
    }
}

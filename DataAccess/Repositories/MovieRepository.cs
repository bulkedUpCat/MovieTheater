using Core.Helpers;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DataAccess.Contexts;
using DataAccess.Dapper;
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
        private readonly AppReadDbConnection _readDbConnection;
        private readonly AppWriteDbConnection _writeDbConnection;

        public MovieRepository(AppDbContext context,
            AppReadDbConnection readDbConnection)
        {
            _context = context;
            _readDbConnection = readDbConnection;
            //_writeDbConnection = writeDbConnection;
        }

        public async Task<IEnumerable<Movie>> GetTest()
        {
            var query = $"SELECT * FROM [Movies] AS m" +
                $" INNER JOIN [MovieGenres] AS mg ON m.Id = mg.MoviesId" +
                $" INNER JOIN [MovieGenre] AS g ON g.Id = mg.GenresId";

            var movies = await _readDbConnection.QueryAsync<Movie>(query);

            return movies;
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

        public async Task<IEnumerable<Movie>> GetPagedMovies()
        {
            var movies = await _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.WatchLaterUsers)
                .Include(m => m.FavoriteListUsers)
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

using Core.Models;
using DAL.Abstractions.Interfaces;
using Dapper;
using DataAccess.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MovieGenreRepository : IMovieGenreRepository
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;

        public MovieGenreRepository(AppDbContext context)
        {
            _context = context;
            _connectionString = "Data Source=DESKTOP-LA5RDNV;Database=MovieTheaterDB2;Trusted_connection=true";
        }

        public async Task<IEnumerable<MovieGenre>> GetAsync
            (Expression<Func<MovieGenre, bool>> filter = null
            , Func<IQueryable<MovieGenre>, IOrderedQueryable<MovieGenre>> orderBy = null
            , string includeProperties = "")
        {
            IQueryable<MovieGenre> query = _context.MovieGenre;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<MovieGenre> GetByIdAsync(object id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new { Id = id };
                var query = "SELECT * FROM [MovieGenre] WHERE Id = @Id";
                var movieGenre = await connection.QuerySingleAsync<MovieGenre>(query,parameters);
                return movieGenre;
            }
        }

        public async Task<MovieGenre> InsertAsync(MovieGenre entity)
        {
            await _context.MovieGenre.AddAsync(entity);
            return entity;
        }

        public void Update(MovieGenre entityToUpdate)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task Delete(object id)
        {
            var movieGenre = await _context.MovieGenre.FindAsync(id);
            Delete(movieGenre);
        }

        public void Delete(MovieGenre entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _context.MovieGenre.Attach(entityToDelete);
            }

            _context.MovieGenre.Remove(entityToDelete);
        }
    }
}

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

        public MovieGenreRepository(AppDbContext context)
        {
            _context = context;
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
            var movieGenre = await _context.MovieGenre.FindAsync(id);
            return movieGenre;
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

        public async Task DeleteByIdAsync(int id)
        {
            var movieGenre = await _context.MovieGenre.FindAsync(id);
            _context.MovieGenre.Remove(movieGenre);
        }
    }
}

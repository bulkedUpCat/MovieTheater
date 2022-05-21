﻿using Core.Helpers;
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

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAsync(Expression<Func<Movie, bool>> filter = null
            , Func<IQueryable<Movie>, IOrderedQueryable<Movie>> orderBy = null
            , string includeProperties = "")
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

        public async Task<Movie> InsertAsync(Movie entity)
        {
            await _context.Movies.AddAsync(entity);
            return entity;
        }

        public void Update(Movie entityToUpdate)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task Delete(object id)
        {
            var movie = await _context.Movies.FindAsync(id);
            Delete(movie);
        }

        public void Delete(Movie entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _context.Movies.Attach(entityToDelete);
            }

            _context.Movies.Remove(entityToDelete);
        }
    }
}

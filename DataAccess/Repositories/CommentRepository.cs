using Core.Models;
using DAL.Abstractions.Interfaces;
using Dapper;
using DataAccess.Contexts;
using DataAccess.Dapper;
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
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAsync(
            Expression<Func<Comment, bool>> filter = null, 
            Func<IQueryable<Comment>, 
            IOrderedQueryable<Comment>> orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<Comment> query = _context.Comments;

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

        public async Task<Comment> GetByIdAsync(object id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetByMovieIdAsync(int id)
        {
            return _context.Comments.Where(c => c.MovieId == id);
        }

        public async Task<Comment> InsertAsync(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
            return entity;
        }

        public void Update(Comment entityToUpdate)
        {
            _context.Comments.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
        }
    }
}

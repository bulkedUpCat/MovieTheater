using Core.Models;
using DAL.Abstractions.Interfaces;
using DataAccess.Contexts;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenericRepository<User> _userRepo;
        private IGenericRepository<Movie> _movieRepo;
        private IGenericRepository<Comment> _commentRepo;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<User> UserRepository {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new GenericRepository<User>(_context);
                }

                return _userRepo;
            } 
        }

        public IGenericRepository<Movie> MovieRepository
        {
            get
            {
                if (_movieRepo == null)
                {
                    _movieRepo = new GenericRepository<Movie>(_context);
                }

                return _movieRepo;
            }
        }

        public IGenericRepository<Comment> CommentRepository { 
            get
            {
                if (_commentRepo == null)
                {
                    _commentRepo = new GenericRepository<Comment>(_context);
                }

                return _commentRepo;
            } 
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

﻿using Core.Models;
using DAL.Abstractions.Interfaces;
using DataAccess.Contexts;
using DataAccess.Dapper;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly AppReadDbConnection _readDbConnection;
        private readonly AppWriteDbConnection _writeDbConnection;
        private IUserRepository _userRepo;
        private IMovieRepository _movieRepo;
        private ICommentRepository _commentRepo;
        private IMovieGenreRepository _movieGenreRepo;
        public UnitOfWork(AppDbContext context,
            AppReadDbConnection readDbConnection,
            AppWriteDbConnection writeDbConnection)
        {
            _context = context;
            _readDbConnection = readDbConnection;
            _writeDbConnection = writeDbConnection;
        }

        public IUserRepository UserRepository {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new UserRepository(_context);
                }

                return _userRepo;
            } 
        }

        public IMovieRepository MovieRepository
        {
            get
            {
                if (_movieRepo == null)
                {
                    _movieRepo = new MovieRepository(_context,_readDbConnection);
                }

                return _movieRepo;
            }
        }

        public ICommentRepository CommentRepository { 
            get
            {
                if (_commentRepo == null)
                {
                    _commentRepo = new CommentRepository(_context, _readDbConnection, _writeDbConnection);
                }

                return _commentRepo;
            } 
        }

        public IMovieGenreRepository MovieGenreRepository
        {
            get
            {
                if (_movieGenreRepo == null)
                {
                    _movieGenreRepo = new MovieGenreRepository(_context);
                }

                return _movieGenreRepo;
            }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}

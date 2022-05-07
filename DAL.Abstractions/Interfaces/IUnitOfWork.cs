using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstractions.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IMovieRepository MovieRepository { get; }
        ICommentRepository CommentRepository { get; }
        IMovieGenreRepository MovieGenreRepository { get; }
        Task SaveChanges();
    }
}

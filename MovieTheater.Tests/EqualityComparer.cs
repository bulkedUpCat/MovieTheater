using Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Tests
{
    internal class MovieEqualityComparer : IEqualityComparer<Movie>
    {
        public bool Equals(Movie? x, Movie? y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id &&
                x.Title == y.Title &&
                x.ReleaseDate == y.ReleaseDate &&
                x.Director == y.Director &&
                x.Description == y.Description;
        }

        public int GetHashCode([DisallowNull] Movie obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class CommentEqualityComparer : IEqualityComparer<Comment>
    {
        public bool Equals(Comment? x, Comment? y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id &&
                x.MovieId == y.MovieId &&
                x.UserId == y.UserId &&
                x.UserName == y.UserName &&
                x.Text == y.Text;
        }

        public int GetHashCode([DisallowNull] Comment obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class MovieGenreEqualityComparer : IEqualityComparer<MovieGenre>
    {
        public bool Equals(MovieGenre? x, MovieGenre? y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id &&
                x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] MovieGenre obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User? x, User? y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id &&
                x.UserName == y.UserName &&
                x.Age == y.Age &&
                x.Email == y.Email;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }
}

using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IMovieService
    {
        string GetAllMovies();
        bool AddMovie(MovieDTO movieDTO);
    }
}

using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IFavoriteListService
    {
        Task<IEnumerable<Movie>> GetFavoriteMoviesOfUserAsync(string id);
        Task<bool> AddToFavoriteListAsync(MovieUser movieUser);
        Task<bool> RemoveFromFavoriteListAsync(MovieUser movieUser);
    }
}

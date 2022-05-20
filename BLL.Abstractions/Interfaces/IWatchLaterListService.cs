using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IWatchLaterListService
    {
        Task<IEnumerable<Movie>> GetWatchLaterMoviesOfUserAsync(string id);
        Task<bool> AddToWatchLaterListAsync(MovieUser movieUser);
        Task<bool> RemoveFromWatchLaterListAsync(MovieUser movieUser);
    }
}

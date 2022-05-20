using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUser(int id);
        Task<User> GetUser(string email);
        Task<User> LogInAsync(UserLoginDTO user);
        Task<User> SignUpAsync(UserRegisterDTO user);
        Task<bool> ConfirmEmailAsync(string token, string email);
        Task<bool> ChangeUsernameAsync(ChangeUsernameDTO changeUsername);
        Task<bool> ChangePasswordAsync(ChangePasswordDTO changePassword);
        Task<bool> ChangeEmailAsync(ChangeEmailDTO changeEmail);
    }
}

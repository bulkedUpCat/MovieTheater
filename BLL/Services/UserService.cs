using BBL.Validators;
using BLL.Abstractions.Interfaces;
using BLL.Validators;
using Core.DTOs;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService //: IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UnitOfWork unitOfWork,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleIdentity)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleIdentity;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.UserRepository.Get();
        }

        public User GetUser(int id)
        {
            return _unitOfWork.UserRepository.Get(u => Int32.Parse(u.Id) == id).First();
        }

        public User GetUser(string email)
        {
            return _unitOfWork.UserRepository.Get(u => u.Email == email).First();
        }

        public async Task<User> LogIn(UserLoginDTO user)
        {
            var foundUser = await _userManager.FindByEmailAsync(user.Email);

            if (foundUser == null)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(foundUser, user.Password);

            return result == false ? null : foundUser;
        }

        public async Task<User> SignUp(UserRegisterDTO user)
        {
            var userExists = await _userManager.FindByEmailAsync(user.Email);

            if (userExists != null)
            {
                return null;
            }

            var newUser = new User()
            {
                Login = user.Login,
                UserName = user.Name,
                Email = user.Email,
                Age = user.Age,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
                return null;
            }

            return newUser;
        }

        public void UpdateEntity(User user)
        {
            try
            {
                _unitOfWork.UserRepository.Update(user);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public IEnumerable<User> Where(Expression<Func<User,bool>> expression)
        {
            return _unitOfWork.UserRepository.Where(expression);
        }

        public User FirstOrDefault(Expression<Func<User, bool>> expression)
        {
            return _unitOfWork.UserRepository.FirstOrDefault(expression);
        }

        public bool Any(Expression<Func<User, bool>> expression)
        {
            return _unitOfWork.UserRepository.Any(expression);
        }
    }
}

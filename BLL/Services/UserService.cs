using BBL.Validators;
using BLL.Abstractions.Interfaces;
using BLL.Validators;
using Core.DTOs;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService //: IUserService
    {
        private readonly UnitOfWork _unitOfWork;

        //private readonly IGenericRepository<User> _repo;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.UserRepository.Get();
        }

        public User GetUser(int id)
        {
            return _unitOfWork.UserRepository.Get(u => u.Id == id).First();
        }

        public User GetUser(string email)
        {
            return _unitOfWork.UserRepository.Get(u => u.Email == email).First();
        }

        public User LogIn(UserLoginDTO user)
        {
            SignInValidator validator = new SignInValidator(this);

            if (!validator.UserPropertiesAreValid(user) ||
                !validator.UserExists(user))
            {
                return null;
            }

            var existingUser = _unitOfWork.UserRepository.FirstOrDefault(u => u.Login == user.Login 
                || u.Email == user.Email);

            if (existingUser == null)
            {            
                return null;
            }

            return existingUser;
        }

        public async Task<User> SignUp(UserRegisterDTO user)
        {
            SignUpValidator validator = new SignUpValidator(this);

            if (!validator.UserPropertiesAreValid(user) ||
                !validator.EmailIsValid(user.Email) ||
                !validator.PasswordIsValid(user.Password) ||
                !validator.PasswordsMatch(user.Password, user.ConfirmPassword))
            {
                return null;
            }

            User newUser = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                Age = user.Age
            };

            try
            {
                _unitOfWork.UserRepository.Insert(newUser);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

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

using Core.DTOs;
using Core.Models;
using MovieTheater.BeautifyUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Components
{
    public static class UserCredentials
    {
        public static UserLoginDTO LogIn()
        {
            UserLoginDTO LoggedUser = new UserLoginDTO();
            BeautifyUserInput beautifyUserInput = new BeautifyUserInput();
            beautifyUserInput.SignInLogin();

            string loginOrEmail = Console.ReadLine();

            if (!string.IsNullOrEmpty(loginOrEmail))
            {
                LoggedUser.Login = loginOrEmail;
                LoggedUser.Email = loginOrEmail;
            }

            beautifyUserInput.SignInPassword();
            
            string password = Console.ReadLine();

            if (!string.IsNullOrEmpty(password))
            {
                LoggedUser.Password = password;
            }

            return (UserLoginDTO)LoggedUser;
        }

        public static UserRegisterDTO SignUp()
        {
            UserRegisterDTO LoggedUser = new UserRegisterDTO();
            BeautifyUserInput beautifyUserInput = new BeautifyUserInput();
            beautifyUserInput.SignUpInput();

            beautifyUserInput.SignUpName();
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
            {
                LoggedUser.Name = name;
            }

            beautifyUserInput.SignUpAge();
            int age = Int32.Parse(Console.ReadLine());
            if (age > 0 && age < 114)
            {
                LoggedUser.Age = age;
            }

            beautifyUserInput.SignUpEmail();
            string email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email))
            {
                LoggedUser.Email = email;
            }

            beautifyUserInput.SignUpLogin();
            string login = Console.ReadLine();
            if (!string.IsNullOrEmpty(login))
            {
                LoggedUser.Login = login;
            }

            beautifyUserInput.SignUpPassword();
            string password = Console.ReadLine();
            if (!string.IsNullOrEmpty(password))
            {
                LoggedUser.Password = password;
            }

            beautifyUserInput.SignUpConfirmPassword();
            string confirmPassword = Console.ReadLine();
            if (!string.IsNullOrEmpty(confirmPassword))
            {
                LoggedUser.ConfirmPassword = confirmPassword;
            }

            return LoggedUser;
        }
    }
}

using BLL.Services;
using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BBL.Validators
{
    public class SignUpValidator
    {
        private readonly UserService _userService;

        public SignUpValidator(UserService userService)
        {
            _userService = userService;
        }

        public bool UserPropertiesAreValid(UserRegisterDTO user)
        {
            if (string.IsNullOrEmpty(user.Name) ||
                string.IsNullOrEmpty(user.Login) ||
                string.IsNullOrEmpty(user.Password) ||
                string.IsNullOrEmpty(user.Email) ||
                string.IsNullOrEmpty(user.ConfirmPassword) ||
                (user.Age < 0 || user.Age > 113) ||
                (user.Password != user.ConfirmPassword)
                )
            {
                return false;
            }

            if (_userService.Any(u => u.Login == user.Login) ||
                _userService.Any(u => u.Email == user.Email))
            {
                return false;
            }

            return true;
        }

        public bool PasswordIsValid(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            bool isValid = hasNumber.IsMatch(password)
                && hasUpperChar.IsMatch(password)
                && hasMinimum8Chars.IsMatch(password);    

            return isValid;
        }

        public bool EmailIsValid(string email)
        {
            string emailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                  + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                  + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            var emailValidator = new Regex(emailPattern, RegexOptions.IgnoreCase);
            bool isValid = emailValidator.IsMatch(email);

            return isValid;
        }

        public bool PasswordsMatch(string password, string confirmedPassword)
        {
            return password == confirmedPassword;
        }
    }
}

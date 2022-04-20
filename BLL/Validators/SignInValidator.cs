using BLL.Services;
using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class SignInValidator
    {
        private readonly UserService _userService;

        public SignInValidator(UserService userService)
        {
            _userService = userService;
        }

        private bool EnteredEmail(string email)
        {
            string emailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                  + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                  + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            var emailValidator = new Regex(emailPattern, RegexOptions.IgnoreCase);
            bool isValid = emailValidator.IsMatch(email);

            return isValid;
        }

        public bool UserPropertiesAreValid(UserLoginDTO user)
        {
            bool isEmail = EnteredEmail(user.Email);

            if ((string.IsNullOrEmpty(user.Login) && !isEmail) ||
                (string.IsNullOrEmpty(user.Email) && isEmail) ||
                string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            return true;
        }

        public bool UserExists(UserLoginDTO user)
        {
            bool isEmail = EnteredEmail(user.Email);
            if ((!_userService.Any(u => u.Login == user.Login) && !isEmail) ||
                (!_userService.Any(u => u.Email == user.Email) && isEmail) ||
                !_userService.Any(u => u.Password == user.Password))
            {
                return false;
            }

            return true;
        }
    }
}

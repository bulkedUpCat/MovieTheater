using BLL.Email;
using Core.DTOs;
using Core.Models;
using DAL.Abstractions.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using NETCore.MailKit.Core;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Services
{
    public class UserService //: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;

        public UserService(IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleIdentity,
            IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleIdentity;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.UserRepository.GetAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<User> GetUser(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> LogInAsync(UserLoginDTO user)
        {
            var foundUser = await _userManager.FindByEmailAsync(user.Email);

            if (foundUser == null)
            {
                return null;
            }

            var result = await _signInManager.PasswordSignInAsync(foundUser, user.Password, false, false);

            if (result.Succeeded)
            {
                return foundUser;
            }

            return null;
        }

        public async Task<User> SignUpAsync(UserRegisterDTO user)
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

            if (result.Succeeded)
            {
                /*var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
                var tokenEncoded = WebEncoders.Base64UrlEncode(tokenBytes);

                var confirmationLink = "https://localhost:7065/api/Auth/ConfirmEmailLink?token=" +
                    tokenEncoded + "&email=" + user.Email;

                var emailTemplate = new EmailTemplate();

                emailTemplate.To = user.Email;
                emailTemplate.UserId = newUser.Id;
                emailTemplate.Link = confirmationLink;
                emailTemplate.Subject = "Email Confirmation for MovieTheater";
                emailTemplate.Body = $"<p>Hi {newUser.UserName}</p>" +
                    $"<p>Please click here to confirm your account</p>" +
                    $"<p>{confirmationLink}</p>" +
                    "Thank you!";

                var emailConfirmed = new EmailSender().SendSmtpMail(emailTemplate);*/

                return newUser;
            }

            return null;
        }


        public async Task<bool> ConfirmEmailAsync(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var tokenEncodedBytes = WebEncoders.Base64UrlDecode(token);
            var tokenDecoded = Encoding.UTF8.GetString(tokenEncodedBytes);

            var result = await _userManager.ConfirmEmailAsync(user, tokenDecoded);

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ChangeUsernameAsync(ChangeUsernameDTO changeUsername)
        {
            if (changeUsername == null)
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(changeUsername.UserId);

            if (user == null)
            {
                return false;
            }

            user.UserName = changeUsername.NewUsername;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDTO changePassword)
        {
            if (changePassword == null)
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(changePassword.UserId);

            if (user == null)
            {
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, changePassword.NewPassword);

            return result.Succeeded;
        }

        public async Task<bool> ChangeEmailAsync(ChangeEmailDTO changeEmail)
        {
            if (changeEmail == null)
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(changeEmail.UserId);

            if (user == null)
            {
                return false;
            }

            user.Email = changeEmail.Email;

            var result =  await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task UpdateEntityAsync(User user)
        {
            try
            {
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}

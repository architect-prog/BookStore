using BookStore.Models;
using BookStore.Services;
using BookStore.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class AccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly EmailService _emailService;
        private readonly Application _application;

        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager, 
            EmailService emailService, IOptions<Application> applicationOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _application = applicationOptions.Value;
        }
        public async Task<IdentityResult> Add(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await GenerateEmailConfirmation(user);                
            }

            return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(User user, string password, bool rememberUser)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, rememberUser, false);

            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }

        public async Task<IdentityResult> ChangePassword(User user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            return result;
        }

        public async Task<IdentityResult> ConfirmEmail(string userId, string token)
        {            
            var result = await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(userId), token);

            return result;
        }

        public async Task GenerateEmailConfirmation(User user)
        {           
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            if (!string.IsNullOrEmpty(token))
            {
                await SendPasswordResetEmail(user, token);
            }
        }

        public async Task GeneratePasswordResetConfirmation(User user)
        {
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (!string.IsNullOrEmpty(token))
            {
                await SendPasswordResetEmail(user, token);
            }
        }

        public async Task<IdentityResult> ResetPassword(User user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result;
        }

        private async Task SendConfirmationEmail(User user, string token)
        {
            UserEmailOptions options = new UserEmailOptions()
            {
                ToEmails = new List<string>() { user.Email },
                Placeholders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{Username}}", user.Firstname),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(_application.AppDomain + _application.EmailConfirmation, user.Id, token))
                }
            };

            await _emailService.SendEmailConfirmation(options);
        }

        private async Task SendPasswordResetEmail(User user, string token)
        {
            UserEmailOptions options = new UserEmailOptions()
            {
                ToEmails = new List<string>() { user.Email },
                Placeholders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{Username}}", user.Firstname),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(_application.AppDomain + _application.ForgotPassword, user.Id, token))
                }
            };

            await _emailService.SendPasswodResetEmail(options);
        }
    }
}

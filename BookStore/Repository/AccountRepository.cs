using BookStore.Models;
using Microsoft.AspNetCore.Identity;
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

        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> Add(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
                        
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

        public async Task<IdentityResult> ChangePassword(User user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            return result;
        }


        public Task<IEnumerable<IdentityUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

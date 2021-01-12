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

        public AccountRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> Add(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
                        
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

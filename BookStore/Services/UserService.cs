using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;

        public UserService(IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public string GetUserId()
        {
            string result = _contextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return result;
        }

        public async Task<User> GetCurrentUser()
        {
            User user = await _userManager.FindByIdAsync(GetUserId());

            return user;
        }

        public bool IsAthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}

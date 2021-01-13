using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Utils
{
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public ClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) 
            : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identityClaims = await base.GenerateClaimsAsync(user);
            identityClaims.AddClaim(new Claim(nameof(user.Firstname), user.Firstname ?? ""));
            identityClaims.AddClaim(new Claim(nameof(user.Lastname), user.Lastname ?? ""));

            return identityClaims;
        }
    }
}

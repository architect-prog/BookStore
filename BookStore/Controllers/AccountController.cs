using BookStore.Models;
using BookStore.Repository;
using BookStore.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository _accountReposirory;

        public AccountController(AccountRepository userReposirory)
        {
            _accountReposirory = userReposirory;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel user)
        {
            if (ModelState.IsValid)
            {
                User identity = new User()
                {
                    Email = user.Email,
                    Firstname = user.FirstName,
                    Lastname = user.LastName,
                    UserName = user.FirstName + " " + user.LastName
                };

                var result = await _accountReposirory.Add(identity, user.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);                       
                    }
                    return View(user);
                }
            }

            return View();
        }
    }
}

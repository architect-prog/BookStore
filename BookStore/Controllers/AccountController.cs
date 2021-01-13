using AutoMapper;
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
        private readonly IMapper _mapper;

        public AccountController(AccountRepository userReposirory, IMapper mapper)
        {
            _accountReposirory = userReposirory;
            _mapper = mapper;
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
                User identity = _mapper.Map<User>(user);

                var result = await _accountReposirory.Add(identity, user.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);                       
                    }
                    return View(user);
                }

                ModelState.Clear();
            }

            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User identity = _mapper.Map<User>(user); 

                var result = await _accountReposirory.PasswordSignInAsync(identity, user.Password, user.RememberMe);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                }

                ModelState.AddModelError("", "Invalid email or password");

            }

            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await _accountReposirory.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}

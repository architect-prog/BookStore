using AutoMapper;
using BookStore.Models;
using BookStore.Repository;
using BookStore.Services;
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
        private readonly UserService _userService;

        public AccountController(AccountRepository userReposirory, IMapper mapper, UserService userService)
        {
            _accountReposirory = userReposirory;
            _mapper = mapper;
            _userService = userService;
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
                return RedirectToAction(nameof(ConfirmEmail), new { email = identity.Email });
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
                else if(result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not allowed login");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }

            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await _accountReposirory.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel passwordChange)
        {
            if (ModelState.IsValid)
            {
                if (_userService.IsAthenticated())
                {
                    User user = await _userService.GetCurrentUser();
                    var result = await _accountReposirory.ChangePassword(user, passwordChange.CurrentPassword, passwordChange.NewPassword);

                    if (result.Succeeded)
                    {
                        ViewBag.Success = true;
                        
                        ModelState.Clear();
                        return View();
                    }
                    
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }                    
                }
            }
            
            return View(passwordChange);
        }

        public async Task<IActionResult> ConfirmEmail(string id, string token, string email)
        {
            EmailConfirmViewModel model = new EmailConfirmViewModel()
            {
                Email = email                
            };

            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');

                var result = await _accountReposirory.ConfirmEmail(id, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmViewModel emailConfirm)
        {
            var user = await _accountReposirory.GetUserByEmail(emailConfirm.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    emailConfirm.IsConfirmed = true;
                    return View(emailConfirm);
                }

                await _accountReposirory.GenerateEmailConfirmation(user);
                emailConfirm.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Something wrong");
            }

            return View(emailConfirm);
        }
    }
}

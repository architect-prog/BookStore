using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.Extensions.Configuration;
using BookStore.Services;
using BookStore.Utils;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;
        private readonly EmailService _emailService;

        public HomeController(IConfiguration configuration, UserService userService, 
            EmailService emailService)
        {
            _configuration = configuration;
            _userService = userService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {      
            return View();
        }

        [Route("about-us")]
        public IActionResult AboutUs()
        {
            return View();
        }

        [Route("contact-us")]
        [Authorize]
        public IActionResult ContactUs()
        {
            return View();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;
        private readonly LanguageRepository _languageRepository;
        private readonly IWebHostEnvironment _environment;
        public BookController(BookRepository bookRepository, 
            LanguageRepository languageRepository, IWebHostEnvironment env)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _environment = env;
        }

        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAll();

            List<BookViewModel> result = new List<BookViewModel>();
            if (books?.Any() != null)
            {
                foreach (var book in books)
                {
                    result.Add(new BookViewModel()
                    {
                        Id = book.Id,
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Language = book.Language,
                        LanguageId = book.LanguageId,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        ImageUrl = book.ImageUrl
                    });
                }
            }

            return View(result);
        }

        [Route("Book-Details")]
        public async Task<IActionResult> GetBook(int id)
        {
            Book book = await _bookRepository.GetById(id);
            BookViewModel result = new BookViewModel()
            {
                Id = book.Id,
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Language = book.Language,
                LanguageId = book.LanguageId,
                Title = book.Title,
                TotalPages = book.TotalPages,
                ImageUrl = book.ImageUrl
            };

            return View(result);
        }

        public async Task<IActionResult> CreateBook(bool isSuccess = false, int newBookId = 0)
        {
            IEnumerable<Language> languages = await _languageRepository.GetAll();
            ViewBag.Languages = languages.Select(x => new SelectListItem(x.Text, x.Id.ToString()));
            
            ViewBag.IsSuccess = isSuccess;
            ViewBag.Id = newBookId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                string imagePath = "images/" + Guid.NewGuid().ToString() + "_" + book.Photo.FileName;
                string savingPath = Path.Combine(_environment.WebRootPath, imagePath);
                imagePath = "/" + imagePath;
                
                Book newBook = new Book()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    LanguageId = book.LanguageId,
                    Title = book.Title,
                    TotalPages = book.TotalPages ?? 0,
                    ImageUrl = imagePath,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow
                };

                int id = await _bookRepository.Add(newBook);
                await book.Photo.CopyToAsync(new FileStream(savingPath, FileMode.Create));

                if (id > 0)
                {
                    return RedirectToAction(nameof(CreateBook), new { isSuccess = true, newBookId = id });
                }
            }

            IEnumerable<Language> languages = await _languageRepository.GetAll();
            ViewBag.Languages = languages.Select(x => new SelectListItem(x.Text, x.Id.ToString()));

            ModelState.AddModelError("", "Incorrect input data");

            return View();
        }

        public List<Book> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }    
    }
}

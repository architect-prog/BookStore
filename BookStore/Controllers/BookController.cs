using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;
        private readonly LanguageRepository _languageRepository;
        public BookController(BookRepository bookRepository, LanguageRepository languageRepository)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
        }

        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAll();            
            return View(books);
        }

        [Route("Book-Details")]
        public async Task<IActionResult> GetBook(int id)
        {
            BookViewModel book = await _bookRepository.GetById(id);
            return View(book);
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
                int id = await _bookRepository.Add(book);
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

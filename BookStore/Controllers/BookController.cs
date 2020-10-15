using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Repository;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;
        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooks();            
            return View(books);
        }

        [Route("Book-Details")]
        public async Task<IActionResult> GetBook(int id)
        {
            Book book = await _bookRepository.GetBookById(id);
            return View(book);
        }

        public IActionResult CreateBook(bool isSuccess = false, int newBookId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.Id = newBookId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
        {
            int id = await _bookRepository.AddNewBook(book);            
            if (id > 0)
            {
                return RedirectToAction(nameof(CreateBook), new { isSuccess = true, newBookId = id });
            }
            return View();
        }

        public List<Book> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }      
    }
}

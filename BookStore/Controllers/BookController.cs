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

        public IActionResult GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();            
            return View(books);
        }

        [Route("Book-Details")]
        public IActionResult GetBook(int id)
        {             
            return View(_bookRepository.GetBookById(id));
        }

        public IActionResult CreateBook(bool isSuccess = false, int newBookId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.Id = newBookId;
            return View();
        }

        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            int id = _bookRepository.AddNewBook(book);            
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

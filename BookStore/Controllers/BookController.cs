using System;
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
using Microsoft.AspNetCore.Http;
using AutoMapper;
using BookStore.ViewModels.BookViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;
        private readonly LanguageRepository _languageRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository, 
            IWebHostEnvironment env, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _environment = env;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAll();

            List<BookThumbnailViewModel> result = new List<BookThumbnailViewModel>();
            if (books?.Any() != null)
            {
                foreach (var book in books)
                {
                    result.Add(_mapper.Map<BookThumbnailViewModel>(book));
                }
            }

            return View(result);
        }

        [Route("~/Book-Details/{id:int:min(1)}")]
        public async Task<IActionResult> GetBook(int id)
        {
            Book book = await _bookRepository.GetById(id);

            BookDetailsViewModel result = _mapper.Map<BookDetailsViewModel>(book);           

            return View(result);
        }

        [Authorize]
        public IActionResult CreateBook(bool isSuccess = false, int newBookId = 0)
        {            
            ViewBag.IsSuccess = isSuccess;
            ViewBag.Id = newBookId;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBook(BookCreateViewModel book)
        {
            if (ModelState.IsValid)
            {
                string imagePath = await SaveImage(book.Image, "bookImages/cover/");
                book.ImageUrl = imagePath;

                book.BookGalery = new List<Gallery>();
                if (book.Gallery != null)
                {
                    foreach (var image in book.Gallery)
                    {
                        Gallery gallery = new Gallery()
                        {
                            Name = image.FileName,
                            ImageUrl = await SaveImage(image, "bookImages/gallery/")
                        };

                        book.BookGalery.Add(gallery);
                    }
                }              

                string previewPath = await SaveImage(book.Preview, "bookPreviews/");
                book.PreviewUrl = previewPath;

                Book newBook = _mapper.Map<Book>(book);

                int id = await _bookRepository.Add(newBook);

                if (id > 0)
                {
                    return RedirectToAction(nameof(CreateBook), new { isSuccess = true, newBookId = id });
                }
            }

            ModelState.AddModelError("", "Incorrect input data");

            return View();
        }     

        public List<Book> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }

        private async Task<string> SaveImage(IFormFile file, string imageFolderPath)
        {
            string imagePath = imageFolderPath + Guid.NewGuid().ToString() + "_" + file.FileName;
            string savingPath = Path.Combine(_environment.WebRootPath, imagePath);
            imagePath = "/" + imagePath;

            await file.CopyToAsync(new FileStream(savingPath, FileMode.Create));
            return imagePath;
        }
    }
}

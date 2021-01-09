using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataContexts;
using BookStore.ViewModels;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository : BaseRepository<BookViewModel>
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> Add(BookViewModel item)
        {
            Book newBook = new Book()
            {
                Author = item.Author,
                Category = item.Category,
                Description = item.Description,
                LanguageId = item.LanguageId,
                Title = item.Title,
                TotalPages = item.TotalPages ?? 0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }   

        public async Task<IEnumerable<BookViewModel>> GetAll()
        {
            var books = await _context.Books.ToListAsync();
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
                        TotalPages = book.TotalPages
                    });
                }
            }
            return result;
        }

        public async Task<BookViewModel> GetById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                BookViewModel result = new BookViewModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Language = book.Language,
                    Title = book.Title,
                    TotalPages = book.TotalPages
                };
                return result;
            }
            return null;
        }

        public List<Book> SearchBook(string title, string author)
        {
            //return DataSource().Where(x => title == x.Title && author == x.Author).ToList();
            return null;
        }   
    }
}

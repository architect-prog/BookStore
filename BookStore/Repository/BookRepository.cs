using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataContexts;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewBook(Book book)
        {
            Books newBook = new Books()
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Language = book.Language,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;            
        }
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            List<Book> result = new List<Book>(); 

            if (books?.Any() != null)
            {
                foreach (var book in books)
                {
                    result.Add(new Book()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Language = book.Language,
                        Title = book.Title,
                        TotalPages = book.TotalPages                       
                    });
                }
            }
            return result;
        }
        public async Task<Book> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                Book result = new Book()
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
            return DataSource().Where(x => title == x.Title && author == x.Author).ToList();
        }
        private List<Book> DataSource()
        {
            return new List<Book>()
            {                
                new Book(){Id =1, Title="MVC", Author = "Nitish", Description="This is the description for MVC book", Category="Programming", Language="English", TotalPages=134 },
                new Book(){Id =3, Title="C#", Author = "Monika", Description="This is the description for C# book", Category="Developer", Language="Hindi", TotalPages=897 },
                new Book(){Id =5, Title="Php", Author = "Webgentle", Description="This is the description for Php book", Category="Programming", Language="English", TotalPages=100 },
                new Book(){Id =6, Title="", Author = "", Description="This is the description for Azure Devops book", Category="DevOps", Language="English", TotalPages=800 },
            };
        }

    }
}

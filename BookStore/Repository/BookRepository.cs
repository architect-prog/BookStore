using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataContexts;
using BookStore.Models;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public int AddNewBook(Book book)
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

            _context.Books.Add(newBook);
            _context.SaveChanges();
            return newBook.Id;            
        }
        public List<Book> GetAllBooks()
        {
            return DataSource();
        }
        public Book GetBookById(int id)
        {
            return DataSource().Where(x => id == x.Id).FirstOrDefault();
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
                new Book(){Id =2, Title="Dot Net Core", Author = "Nitish", Description="This is the description for Dot Net Core book", Category="Framework", Language="Chinese", TotalPages=567 },
                new Book(){Id =3, Title="C#", Author = "Monika", Description="This is the description for C# book", Category="Developer", Language="Hindi", TotalPages=897 },
                new Book(){Id =4, Title="Java", Author = "Webgentle", Description="This is the description for Java book", Category="Concept", Language="English", TotalPages=564 },
                new Book(){Id =5, Title="Php", Author = "Webgentle", Description="This is the description for Php book", Category="Programming", Language="English", TotalPages=100 },
                new Book(){Id =6, Title="Azure DevOps", Author = "Nitish", Description="This is the description for Azure Devops book", Category="DevOps", Language="English", TotalPages=800 },
            };
        }

    }
}

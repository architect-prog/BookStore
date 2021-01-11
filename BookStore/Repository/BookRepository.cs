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
    public class BookRepository : BaseRepository<Book>
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Book item)
        {          
            await _context.Books.AddAsync(item);
            await _context.SaveChangesAsync();

            return item.Id;
        }   

        public async Task<IEnumerable<Book>> GetAll()
        {
            List<Book> books = await _context.Books.ToListAsync(); 
            
            return books;
        }

        public async Task<Book> GetById(int id)
        {            
            Book book = await _context.Books.FindAsync(id);

            return book;
        }

        public List<Book> SearchBook(string title, string author)
        {
            //return DataSource().Where(x => title == x.Title && author == x.Author).ToList();
            return null;
        }   
    }
}

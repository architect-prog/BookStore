using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataContexts
{
    public class BookStoreContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Gallery> Galleries { get; set; }

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookStore.DataContexts
{
    public class BookStoreContext : IdentityDbContext
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

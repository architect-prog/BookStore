using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataContexts
{
    public class BookStoreContext: DbContext
    {
        public DbSet<Books> Books { get; set; }
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {

        }
    }
}

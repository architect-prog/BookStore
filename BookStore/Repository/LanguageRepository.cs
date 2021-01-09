using BookStore.DataContexts;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class LanguageRepository : BaseRepository<Language>
    {
        private readonly BookStoreContext _context;
        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Language item)
        {
            await _context.Languages.AddAsync(item);
            await _context.SaveChangesAsync();

            return item.Id;
        }

        public async Task<IEnumerable<Language>> GetAll()
        {
            List<Language> languages = await _context.Languages.ToListAsync();

            return languages;
        }

        public async Task<Language> GetById(int id)
        {
            Language result = await _context.Languages.FindAsync(id);

            return result;
        }
    }
}

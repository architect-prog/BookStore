using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels.BookViewModels
{
    public class BookDetailsViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Language Language { get; set; }
        public int TotalPages { get; set; }
        public string PreviewUrl { get; set; }
        public List<Gallery> GalleryFiles { get; set; }
    }
}

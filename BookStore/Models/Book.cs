using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public int TotalPages { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<Gallery> BookGalery { get; set; }
        public string PreviewUrl { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

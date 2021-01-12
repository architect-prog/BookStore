using BookStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels.BookViewModels
{
    public class BookCreateViewModel
    {
        [Required(ErrorMessage = "Enter the title of your book")]
        [StringLength(40, MinimumLength = 2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter the author name")]
        public string Author { get; set; }

        [StringLength(255, MinimumLength = 10)]
        public string Description { get; set; }
        public string Category { get; set; }

        [Required(ErrorMessage = "Please select language")]
        public int LanguageId { get; set; }


        [Display(Name = "Total book pages")]
        [Required(ErrorMessage = "Enter the total pages number")]
        public int TotalPages { get; set; }

        [Display(Name = "Main photo")]
        [Required(ErrorMessage = "The main photo is required")]
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Book photos")]
        public IFormFileCollection Gallery { get; set; }
        public virtual ICollection<Gallery> BookGalery { get; set; }

        [Display(Name = "Book text preview")]
        public IFormFile Preview { get; set; }
        public string PreviewUrl { get; set; }
    }
}

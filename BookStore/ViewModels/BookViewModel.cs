using BookStore.Models;
using BookStore.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter the title of your book")]
        [StringLength(40, MinimumLength = 2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter the author name")] 
        public string Author { get; set; }

        [StringLength(255, MinimumLength = 10)]
        public string Description { get; set; }
        public string Category { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }             


        [Display(Name = "Total book pages")]
        [Required(ErrorMessage = "Enter the total pages number")] 
        public int? TotalPages { get; set; }

        [Display(Name = "Main photo")]
        [Required(ErrorMessage = "The main photo is required")]
        public IFormFile Photo { get; set; }
        public string ImageUrl { get; set; }

        public IFormFileCollection Gallery { get; set; }
        public List<Gallery> GalleryFiles { get; set; }
    }
}

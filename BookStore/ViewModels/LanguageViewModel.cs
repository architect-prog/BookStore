using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class LanguageViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        private string _title;
        private string _author;
        public int Id { get; set; }
        public string Title { get => _title; set => _title = value; }
        public string Author { get => _author; set => _author = value; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public int TotalPages { get; set; }
    }
}

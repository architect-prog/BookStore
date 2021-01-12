using AutoMapper;
using BookStore.Models;
using BookStore.ViewModels.BookViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Utils
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookThumbnailViewModel>();
            CreateMap<Book, BookDetailsViewModel>().
                ForMember(dest => dest.GalleryFiles, opt => opt.MapFrom(src => src.BookGalery.ToList()));
            CreateMap<BookCreateViewModel, Book>();
        }
    }
}

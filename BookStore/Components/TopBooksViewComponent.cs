using BookStore.Repository;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.ViewModels.BookViewModels;
using AutoMapper;

namespace BookStore.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly BookRepository _bookRepository;
        private readonly IMapper _mapper;

        public TopBooksViewComponent(BookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await _bookRepository.GetTopBooks(count);

            List<BookThumbnailViewModel> result = new List<BookThumbnailViewModel>();
            if (books?.Any() != null)
            {
                foreach (var book in books)
                {
                    result.Add(_mapper.Map<BookThumbnailViewModel>(book));
                }
            }

            return View(result);
        }
    }
}

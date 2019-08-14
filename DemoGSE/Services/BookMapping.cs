using DemoGSE.Models;
using DemoGSE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGSE.Services
{
    public class BookMapping : IBookMapping
    {
        #region Mapping Methods

        public BookViewModel MapToViewModel(Book book)
        {
            BookViewModel viewModel = new BookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                CreationDate = book.CreationDate
            };
            return viewModel;
        }

        public Book MapToModel(BookViewModel viewbook)
        {
            Book model = new Book()
            {
                Id = viewbook.Id,
                Title = viewbook.Title,
                Price = viewbook.Price,
                CreationDate = viewbook.CreationDate
            };
            return model;
        }

        public Book MapToModelCreate(BookCreateViewModel viewbook)
        {
            Book model = new Book()
            {                
                Title = viewbook.Title,
                Price = viewbook.Price,
                CreationDate = viewbook.CreationDate
            };
            return model;
        }

        #endregion
    }
}

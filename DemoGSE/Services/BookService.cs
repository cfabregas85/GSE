using AutoMapper;
using DemoGSE.Contexts;
using DemoGSE.Models;
using DemoGSE.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGSE.Services
{
    public class BookService : IBookService
    {
        #region Attributes

        private readonly ApplicationDbContext _context;
        private readonly IBookMapping _bookMapping;

        #endregion

        #region Ctr
        public BookService(ApplicationDbContext context, IBookMapping bookMapping)
        {
            this._context = context;
            this._bookMapping = bookMapping;
        }
        #endregion

        #region Add/Edit/List/Delete

        public async Task<List<BookViewModel>> GetAllBooks()
        {            
            var modelbooks = await _context.Book.ToListAsync();
            List<BookViewModel> listDest = modelbooks.Select(x => _bookMapping.MapToViewModel(x)).ToList();
            return listDest;
        }

        public async Task<BookViewModel> GetBook(int id)
        {
            var model = await _context.Book.FirstOrDefaultAsync(x => x.Id == id);
            return _bookMapping.MapToViewModel(model);        
        }

        public async Task AddBook(BookCreateViewModel book)
        {
            var bookModel = _bookMapping.MapToModelCreate(book);
            _context.Add(bookModel);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EditBook(BookViewModel book)
        {
            if (book != null)
            {
               var model = await _context.Book.FirstOrDefaultAsync(x => x.Id == book.Id);

                model.Title = book.Title;
                model.Price = book.Price;
                model.CreationDate = book.CreationDate;
                _context.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            else { return false; }                                    
        }
        
        public async Task<bool> DeleteBook(int id)
        {
            var book = await _context.Book.FirstOrDefaultAsync(x => x.Id == id);
            if (book != null)
            {
                _context.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;           
        }

        #endregion
    }
}

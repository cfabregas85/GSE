using System.Collections.Generic;
using System.Threading.Tasks;
using DemoGSE.ViewModels;

namespace DemoGSE.Services
{
    public interface IBookService
    {
        Task AddBook(BookCreateViewModel book);
        Task<bool> DeleteBook(int id);
        Task<bool> EditBook(BookViewModel book);
        Task<List<BookViewModel>> GetAllBooks();
        Task<BookViewModel> GetBook(int id);
    }
}
using DemoGSE.Models;
using DemoGSE.ViewModels;

namespace DemoGSE.Services
{
    public interface IBookMapping
    {
        Book MapToModel(BookViewModel viewbook);
        Book MapToModelCreate(BookCreateViewModel viewbook);
        BookViewModel MapToViewModel(Book book);
    }
}
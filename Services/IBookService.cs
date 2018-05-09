using BookCave.Models.InputModels;

namespace BookCave.Services
{
    public interface IBookService
    {
        void ProcessBook(BookInputModel book);
    }
}
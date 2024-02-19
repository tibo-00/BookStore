using BookStore.Data;
using BookStore.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooksLazy();
        IEnumerable<Book> GetAllBooks();
        IEnumerable<CatalogusViewModel> Catalog();
        IEnumerable<CatalogusViewModel> ToCatalogVM(IEnumerable<Book> books);
        IEnumerable<CatalogusViewModel> SearchCatalog(string searchString, int? bookGenre, bool byTitle, bool byAuthor, bool byISBN);
        Book GetBookById(int id);
        int CreateBook(BookViewModel model, Author author, Genre genre, string uniqueFileName);
        int EditBook(BookViewModel model, int id, string? fileName);
        int DeleteBook(int id);
    }
}

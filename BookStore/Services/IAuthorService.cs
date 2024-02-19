using BookStore.Data;
using BookStore.Models.Author;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public interface IAuthorService
    {
        int CreateAuthor(AuthorViewModel model);
        Author GetAuthor(int id);
        String GetAuthorFullName(int id);
        int DeleteAuthor(int id);
        int EditAuthor(AuthorViewModel model, int id);
        IEnumerable<Author> GetAllAuthors();
        IEnumerable<AuthorListViewModel> GetAuthorListViewModels();
    }
}

using BookStore.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public interface IGenreService
    {
        IEnumerable<Genre> GetAllGenres();
        Genre GetGenre(int id);
    }
}

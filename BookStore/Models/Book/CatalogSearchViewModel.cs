using BookStore.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Models.Book
{
    public class CatalogSearchViewModel
    {
        public IEnumerable<CatalogusViewModel> Catalog { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public int? BookGenre { get; set; }
        public string SearchString { get; set; }
        public bool ByTitle { get; set; }
        public bool ByAuthor { get; set; }
        public bool ByISBN { get; set; }
    }
}

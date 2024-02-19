using BookStore.Data;
using BookStore.Models.Author;
using BookStore.Models.Book;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private readonly IBookService _bookService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BookController(IAuthorService authorService, IGenreService genreService, IBookService bookService, IHostingEnvironment environment)
        {
            _authorService = authorService;
            _genreService = genreService;
            _bookService = bookService;
            _hostingEnvironment = environment;
        }

        public ActionResult Details(int id)
        {
            Book book = _bookService.GetBookById(id);
            BookDetailViewModel model = new BookDetailViewModel();
            model.Id = id;
            model.Title = book.Title; 
            model.Author = book.Author;
            model.Genre = book.Genre;
            model.Isbn13 = book.Isbn13;
            model.Pages = book.Pages;
            model.Format = book.Format;
            model.Price = book.Price;
            model.ImageURL = book.ImageURL;
            return View(model);
        }

        [Authorize(Policy = "CanManageCatalog")]
        public ActionResult Create()
        {
            IEnumerable<Author> authors = _authorService.GetAllAuthors();
            IEnumerable<Genre> genres = _genreService.GetAllGenres();
            BookViewModel model = new BookViewModel(authors, genres);
            return View(model);
        }

        [Authorize(Policy = "CanManageCatalog")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyToAsync(fileStream);
                }

                Author author = _authorService.GetAuthor((int)model.SelectedAuthor);
                Genre genre = _genreService.GetGenre((int)model.SelectedGenre);
                _bookService.CreateBook(model, author, genre, uniqueFileName);
                return RedirectToAction("Catalog");
            }
            IEnumerable<Author> authors = _authorService.GetAllAuthors();
            IEnumerable<Genre> genres = _genreService.GetAllGenres();
            model.Authors = authors;
            model.Genres = genres;
            return View(model);
        }

        public ActionResult Catalog(string searchString, int? bookGenre, bool byTitle, bool byAuthor, bool byISBN)
        {
            IEnumerable<CatalogusViewModel> bookCatalog = _bookService.SearchCatalog(searchString, bookGenre, byTitle, byAuthor, byISBN);
            CatalogSearchViewModel model = new CatalogSearchViewModel();
            model.Catalog = bookCatalog;
            model.Genres = _genreService.GetAllGenres();
            return View(model);
        }

        [Authorize(Policy = "CanManageCatalog")]
        public ActionResult Edit(int id)
        {
            IEnumerable<Author> authors = _authorService.GetAllAuthors();
            IEnumerable<Genre> genres = _genreService.GetAllGenres();

            BookViewModel model = new BookViewModel(authors, genres);
            Book book = _bookService.GetBookById(id);

            model.Title = book.Title;
            model.Isbn13 = book.Isbn13;
            model.Pages = book.Pages;
            model.Format = book.Format;
            model.Price = book.Price;
            model.SelectedAuthor = book.AuthorId;
            model.SelectedGenre= book.GenreId;
            return View(model);
        }

        [Authorize(Policy = "CanManageCatalog")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookViewModel model)
        {
            string uniqueFileName = null;
            if (model.ImageFile != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            }
            _bookService.EditBook(model, id, uniqueFileName);
            return RedirectToAction("Catalog");
        }

        [Authorize(Policy = "CanManageCatalog")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return RedirectToAction("Catalog");
        }
    }
}

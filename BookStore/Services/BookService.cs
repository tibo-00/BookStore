using BookStore.Data;
using BookStore.Models.Book;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAllBooksLazy()
        {
            return _context.Books.ToList();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .ToList();
        }

        public IEnumerable<CatalogusViewModel> Catalog()
        {
            IEnumerable<Book> books = GetAllBooks();
            return ToCatalogVM(books);
        }

        public IEnumerable<CatalogusViewModel> ToCatalogVM(IEnumerable<Book> books)
        {
            IEnumerable<CatalogusViewModel> catalog = books.Select(book => new CatalogusViewModel
            {
                Id = book.Id,
                Title = book.Title,
                FirstName = book.Author.FirstName,
                LastName = book.Author.LastName,
                Price = book.Price,
                ImageURL = book.ImageURL,
                GenreName = book.Genre.Name
            });

            return catalog.ToList();
        }

        public Book GetBookById(int id)
        {
            Book book = _context.Books
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .SingleOrDefault(x => x.Id == id);
            return book;
        }

        public int CreateBook(BookViewModel model, Author author,Genre genre, string uniqueFileName)
        {
            Book book = new Book();
            book.Title = model.Title;
            book.Isbn13= model.Isbn13;
            book.Pages= model.Pages;
            book.Format= model.Format;
            book.Price= model.Price;
            book.Author= author;
            book.Genre= genre;
            book.ImageURL = uniqueFileName;

            _context.Books.Add(book);
            _context.SaveChanges();

            return book.Id;
        }

        public int EditBook(BookViewModel model, int id, string? fileName)
        {
            Book existingBook = GetBookById(id);
            existingBook.Title = model.Title;
            existingBook.Isbn13 = model.Isbn13;
            existingBook.Pages = model.Pages;
            existingBook.Format = model.Format;
            existingBook.Price = model.Price;
            existingBook.AuthorId  = model.SelectedAuthor;
            existingBook.GenreId = model.SelectedGenre;
            if(fileName!= null)
            {
                existingBook.ImageURL = fileName;
            }

            _context.Update(existingBook);
            _context.SaveChanges();

            return existingBook.Id;
        }

        public int DeleteBook(int id)
        {
            Book toRemove = GetBookById(id);
            _context.Remove(toRemove);
            _context.SaveChanges();
            return id;
        }

        public IEnumerable<CatalogusViewModel> SearchCatalog(string searchString, int? bookGenre, bool byTitle, bool byAuthor, bool byISBN)
        {
            //var predicate = PredicateBuilder.New<Book>(true);

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    predicate.Or(x => x.Title.Contains(searchString));
            //    predicate.And(x => (x.Author.FirstName + " " + x.Author.LastName).Contains(searchString));
            //    predicate.Or(x => x.Genre.Name.Contains(searchString));
            //    predicate.Or(x => x.Isbn13.Contains(searchString));
            //}

            //if (bookGenre.HasValue)
            //{
            //    predicate.And(x => x.Genre.Id == bookGenre.Value);
            //}

            //if (byTitle || byAuthor || byISBN)
            //{
            //    predicate.And(x => x.Title.Contains(searchString));
            //    predicate.And(x => (x.Author.FirstName + " " + x.Author.LastName).Contains(searchString));
            //    predicate.And(x => x.Isbn13.Contains(searchString));
            //}

            //// Apply search logic
            //IEnumerable <Book> books = _context.Books
            //    .Include(x => x.Author)
            //    .Include(x => x.Genre)
            //    .AsExpandable()
            //    .Where(predicate)
            //    .ToList();

            //IEnumerable<CatalogusViewModel> model = ToCatalogVM(books);

            //return model;

            // Create a query for books
            var predicate = PredicateBuilder.New<Book>(true);

            if (!string.IsNullOrEmpty(searchString))
            {
                if (byTitle)
                {
                    predicate = predicate.And(x => x.Title.Contains(searchString));
                }
                if (byAuthor)
                {
                    predicate = predicate.And(x => (x.Author.FirstName + " " + x.Author.LastName).Contains(searchString));
                }
                if (byISBN)
                {
                    predicate = predicate.And(x => x.Isbn13.Contains(searchString));
                }
            }

            if (bookGenre.HasValue)
            {
                predicate = predicate.And(x => x.Genre.Id == bookGenre.Value);
            }

            // Apply search logic
            IEnumerable<Book> books = _context.Books
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .AsExpandable()
                .Where(predicate)
                .ToList();

            IEnumerable<CatalogusViewModel> model = ToCatalogVM(books);

            return model;
        }
    }
}

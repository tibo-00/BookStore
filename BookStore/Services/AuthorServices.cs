using BookStore.Data;
using BookStore.Models.Author;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class AuthorServices : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public int CreateAuthor(AuthorViewModel model)
        {
            Author author = new Author();
            author.FirstName = model.FirstName;
            author.LastName = model.LastName;
            author.BirthDate = model.BirthDate.Value;

            _context.Authors.Add(author);
            _context.SaveChanges();

            return author.Id;
        }

        public Author GetAuthor(int id)
        {
            return _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
        }

        public String GetAuthorFullName(int id)
        {
            Author author = _context.Authors.Find(id);
            return $"{author.FirstName} {author.LastName}";
        }

        public int DeleteAuthor(int id)
        {
            Author toRemove = _context.Authors.Find(id);
            _context.Remove(toRemove);
            _context.SaveChanges();
            return id;
        }

        public int EditAuthor(AuthorViewModel model, int id)
        {
            Author existingAuthor = GetAuthor(id);
            existingAuthor.FirstName = model.FirstName;
            existingAuthor.LastName = model.LastName;
            existingAuthor.BirthDate = model.BirthDate.Value;

            _context.Update(existingAuthor);
            _context.SaveChanges();

            return existingAuthor.Id;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _context.Authors.Select(x => new Author()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate
            }).ToList();
        }


        public IEnumerable<AuthorListViewModel> GetAuthorListViewModels()
        {
            return _context.Authors.Select(x => new AuthorListViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate
            }).ToList();
        }
    }
}

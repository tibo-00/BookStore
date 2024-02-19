using BookStore.Data;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStore.Models.Book
{
    public class BookViewModel
    {
        [Required(ErrorMessage = "A Book title is required!")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Isbn13Validation(ErrorMessage = "Correct ISBN-13 is required!")]
        [Display(Name = "ISBN-13")]
        public string Isbn13 { get; set; }
        [Required(ErrorMessage ="The number of pages is required!")]
        [Display(Name = "Number of pages")]
        public int Pages { get; set; }
        [Required(ErrorMessage = "Choosing a format is required!")]
        public Format Format { get; set; }
        [Required(ErrorMessage = "A price is required!")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Choosing an author is required!")]
        [Display(Name = "Author")]
        public int? SelectedAuthor { get; set; }
        public IEnumerable<Data.Author>? Authors { get; set; }
        [Required(ErrorMessage = "Choosing a genre is required!")]
        [Display(Name = "Genre")]
        public int? SelectedGenre { get; set; }
        public IEnumerable<Genre>? Genres { get; set; }
        [Required(ErrorMessage = "Cover Image is required!")]
        [Display(Name = "Cover Image")]
        public IFormFile ImageFile { set; get; }
        public BookViewModel(IEnumerable<Data.Author> authors, IEnumerable<Genre> genres)
        {
            Authors = authors;
            Genres = genres;
        }
        public BookViewModel(){}
    }
}

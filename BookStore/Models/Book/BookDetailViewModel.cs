using BookStore.Data;

namespace BookStore.Models.Book
{
    public class BookDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Data.Author Author { get; set; }
        public string Isbn13 { get; set; }
        public Genre Genre { get; set; }
        public int Pages { get; set; }
        public Format Format { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
    }
}

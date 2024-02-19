using Microsoft.Data.SqlClient.Server;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn13 { get; set; }
        public int Pages { get; set; }
        public Format Format { get; set; }
        public double Price { get; set; }
        public string? ImageURL { get; set; }
        [ForeignKey("AuthorId")]
        public int? AuthorId { get; set; }
        public Author Author { get; set; }
        [ForeignKey("GenreId")]
        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}

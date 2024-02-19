namespace BookStore.Models.Book
{
    public class CatalogusViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Price { get; set; }
        public string? ImageURL { get; set; }
        public string GenreName { get; set; }
    }
}

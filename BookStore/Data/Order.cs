namespace BookStore.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public int? Number { get; set; }
        public int? Zip { get; set; }
        public string City { get; set; }
        public string UserId { get; set; }
    }
}

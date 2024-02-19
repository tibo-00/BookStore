using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Data
{
    public class OrderLine
    {
        public int Id { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        [ForeignKey("BookId")]
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}

namespace BookStore.Models.Cart
{
    public class ShoppingCart
    {
        public List<CartLine> CartLines { get; set; } = new List<CartLine>();
    }
}

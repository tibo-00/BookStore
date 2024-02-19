namespace BookStore.Models.Cart
{
    public class CartViewModel
    {
        public List<CartTableViewModel> books { get; set; } = new List<CartTableViewModel>();
        public double Total { get; set; }
    }
}

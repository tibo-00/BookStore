using BookStore.Models.Cart;

namespace BookStore.Models.Order
{
    public class CheckoutViewModel
    {
        public List<OverviewViewModel> Overviews { get; set; } = new List<OverviewViewModel>();
        public double Total { get; set; }
        public DeliveryDetailsViewModel DeliveryDetails { get; set; }
    }
}

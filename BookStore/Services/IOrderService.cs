using BookStore.Data;
using BookStore.Models.Order;

namespace BookStore.Services
{
    public interface IOrderService
    {
        int CreateOrder(CheckoutViewModel model, string userId);
        int CreateOrderLine(int OrderId, int BookId, OverviewViewModel model, double price);
    }
}

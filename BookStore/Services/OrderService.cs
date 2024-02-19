using BookStore.Data;
using BookStore.Data.Migrations;
using BookStore.Models.Order;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace BookStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public int CreateOrder(CheckoutViewModel model, string userId)
        {
            Order order = new Order();

            order.FirstName = model.DeliveryDetails.FirstName;
            order.LastName = model.DeliveryDetails.LastName;
            order.Street = model.DeliveryDetails.Street;
            order.Number = model.DeliveryDetails.Number;
            order.Zip = model.DeliveryDetails.Zip;
            order.City = model.DeliveryDetails.City;
            order.UserId = userId;

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

        public int CreateOrderLine(int orderId, int bookId, OverviewViewModel model, double price)
        {
            OrderLine orderLine = new OrderLine();

            orderLine.OrderId = orderId;
            orderLine.BookId = bookId;
            orderLine.Quantity = model.Quantity;
            orderLine.Price = (float)price;

            _context.OrderLines.Add(orderLine);
            _context.SaveChanges();
            return orderLine.Id;
        }
    }
}

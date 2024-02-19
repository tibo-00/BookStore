using BookStore.Data;
using BookStore.Identity;
using BookStore.Models.Author;
using BookStore.Models.Cart;
using BookStore.Models.Order;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookService _bookService;
        private readonly IOrderService _orderService;

        public OrderController(UserManager<ApplicationUser> userManager, IBookService bookService, IOrderService orderService)
        {
            _userManager = userManager;
            _bookService = bookService;
            _orderService = orderService;
        }


        public async Task<IActionResult> Checkout()
        {
            string cartJson = HttpContext.Session.GetString("cart");
            ShoppingCart cart = !string.IsNullOrEmpty(cartJson) ? JsonSerializer.Deserialize<ShoppingCart>(cartJson) : new ShoppingCart();

            CheckoutViewModel checkoutModel = new CheckoutViewModel();

            foreach (CartLine cartLine in cart.CartLines)
            {
                OverviewViewModel overview = new OverviewViewModel();
                Book book = _bookService.GetBookById(cartLine.ProductId);
                overview.Id = cartLine.ProductId;
                overview.Title = book.Title;
                overview.Quantity = cartLine.Quantity;
                checkoutModel.Overviews.Add(overview);
                checkoutModel.Total += (book.Price * cartLine.Quantity);
            }

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            DeliveryDetailsViewModel deliveryDetails = new DeliveryDetailsViewModel();
            deliveryDetails.FirstName = user.FirstName;
            deliveryDetails.LastName = user.LastName;
            checkoutModel.DeliveryDetails = deliveryDetails;

            return View(checkoutModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = _userManager.GetUserId(User);
                int orderId = _orderService.CreateOrder(model, userId);
                foreach(OverviewViewModel item in model.Overviews)
                {
                    double price = _bookService.GetBookById(item.Id).Price;
                    _orderService.CreateOrderLine(orderId, item.Id ,item, price);
                }
                HttpContext.Session.Clear();
                return Redirect("Confirmed");
            }

            return View(model);
        }

        public ActionResult Confirmed()
        {
            return View();
        }
    }
}

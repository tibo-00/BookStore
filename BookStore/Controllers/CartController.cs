using BookStore.Data;
using BookStore.Models;
using BookStore.Models.Cart;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookService _bookService;
        public CartController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            string cartJson = HttpContext.Session.GetString("cart");
            ShoppingCart cart = !string.IsNullOrEmpty(cartJson) ? JsonSerializer.Deserialize<ShoppingCart>(cartJson) : new ShoppingCart();

            CartViewModel model = new CartViewModel();

            foreach (CartLine cartLine in cart.CartLines)
            {
                CartTableViewModel tableModel = new CartTableViewModel();
                Book book = _bookService.GetBookById(cartLine.ProductId);
                tableModel.Id = book.Id;
                tableModel.Title = book.Title;
                tableModel.FirstName = book.Author.FirstName;
                tableModel.LastName = book.Author.LastName;
                tableModel.Isbn = book.Isbn13;
                tableModel.Price = book.Price;
                tableModel.Quantity = cartLine.Quantity;
                tableModel.Subtotal = (book.Price * cartLine.Quantity);

                model.books.Add(tableModel);
                model.Total += tableModel.Subtotal;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id,[FromForm] int quantity)
        {
            Book book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("cart") ?? new ShoppingCart();

            CartLine cartLine = cart.CartLines.FirstOrDefault(x => x.ProductId == id);
            if(cartLine == null)
            {
                cartLine = new CartLine();
                cartLine.ProductId = id;
                cartLine.Quantity = quantity;
                cart.CartLines.Add(cartLine);
            }
            else
            {
                cartLine.Quantity = quantity;
            }

            HttpContext.Session.SetObjectAsJson("cart", cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int id)
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("cart") ?? new ShoppingCart();
            if (cart == null)
            {
                return NotFound();
            }

            CartLine cartLine = cart.CartLines.FirstOrDefault(x => x.ProductId == id);
            if (cartLine != null)
            {
                cart.CartLines.Remove(cartLine);
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Clear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

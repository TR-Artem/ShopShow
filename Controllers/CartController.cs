using Microsoft.AspNetCore.Mvc;
using ShopShowcase.Models;

namespace ShopShowcase.Controllers
{
    public class CartController : Controller
    {
        // GET /Cart
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetCart();

            var vm = new CartViewModel
            {
                Items = cart,
                JustAdded = TempData["JustAdded"] as bool? ?? false
            };

            return View(vm);
        }

        // POST /Cart/Add
        [HttpPost]
        public IActionResult Add(int productId)
        {
            var product = ProductCatalog.All.FirstOrDefault(p => p.Id == productId);
            if (product is null || product.Stock == 0)
            {
                return RedirectToAction("Index", "Catalog");
            }

            var cart = HttpContext.Session.GetCart();
            var existing = cart.FirstOrDefault(i => i.ProductId == productId);

            if (existing is not null)
            {
                existing.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            HttpContext.Session.SaveCart(cart);
            TempData["JustAdded"] = true;

            return RedirectToAction("Index");
        }

        // POST /Cart/Checkout — вызывается кнопкой «Подтвердить» в модалке
        [HttpPost]
        public IActionResult Checkout()
        {
            // В реальном проекте здесь было бы создание заказа в БД.
            HttpContext.Session.SaveCart(new List<CartItem>());
            return RedirectToAction("ThankYou");
        }

        // GET /Cart/ThankYou
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}

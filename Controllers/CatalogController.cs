using Microsoft.AspNetCore.Mvc;
using ShopShowcase.Models;

namespace ShopShowcase.Controllers
{
    public class CatalogController : Controller
    {
        // GET /Catalog?tag=Электроника
        public IActionResult Index(string? tag)
        {
            var products = string.IsNullOrEmpty(tag)
                ? ProductCatalog.All
                : ProductCatalog.All.Where(p => p.Tag == tag).ToList();

            var vm = new CatalogViewModel
            {
                Products = products,
                Tags = ProductCatalog.Tags,
                ActiveTag = tag
            };

            return View(vm);
        }

        // GET /Catalog/Search?query=ноут
        // Отдаёт HTML-фрагмент (partial), а не целую страницу — используется search.js
        [HttpGet]
        public IActionResult Search(string? query)
        {
            var products = ProductCatalog.All.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.ToLower();
                products = products.Where(p =>
                    p.Name.ToLower().Contains(query) ||
                    p.Description.ToLower().Contains(query));
            }

            return PartialView("_ProductList", products.ToList());
        }

        // POST /Catalog/AddToCart
        // Отдаёт JSON — используется cart.js. Работает с той же корзиной в сессии,
        // что и обычная форма на странице /Cart (см. SessionExtensions/CartItem),
        // поэтому AJAX-добавление и страница корзины остаются согласованными.
        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var product = ProductCatalog.All.FirstOrDefault(p => p.Id == id);

            if (product is null || product.Stock == 0)
            {
                return Json(new { success = false, message = "Товар не найден или закончился" });
            }

            var cart = HttpContext.Session.GetCart();
            var existing = cart.FirstOrDefault(i => i.ProductId == id);

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

            return Json(new
            {
                success = true,
                cartCount = cart.Sum(i => i.Quantity),
                productName = product.Name
            });
        }

        // GET /Catalog/GetCartCount
        // Отдаёт текущее количество товаров в корзине — для обновления бейджа при загрузке страницы
        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = HttpContext.Session.GetCart();
            return Json(new { count = cart.Sum(i => i.Quantity) });
        }
    }
}

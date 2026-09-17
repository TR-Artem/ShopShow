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
    }
}

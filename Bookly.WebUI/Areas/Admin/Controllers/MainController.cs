using Bookly.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class MainController : Controller
    {
        private readonly DatabaseContext _context;

        public MainController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Products = _context.Products.ToList();

            ViewBag.ProductsCount = _context.Products.Count();
            ViewBag.CategoriesCount = _context.Categories.Count();
            ViewBag.BrandsCount = _context.Brands.Count();
            ViewBag.UsersCount = _context.AppUsers.Count();

            return View();
        }
    }
}

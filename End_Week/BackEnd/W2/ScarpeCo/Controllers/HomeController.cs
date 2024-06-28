using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ScarpeCo.Models;
using ScarpeCo.Data;

namespace ScarpeCo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = ProductRepository.GetProducts();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var product = ProductRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Admin()
        {
            var products = ProductRepository.GetProducts();
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            ProductRepository.RemoveProduct(id);
            return RedirectToAction(nameof(Admin));
        }

        public IActionResult Edit(int id)
        {
            var product = ProductRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.UpdateProduct(product);
                return RedirectToAction(nameof(Admin));
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Purchase(int id)
        {
            ProductRepository.PurchaseProduct(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Unpurchase(int id)
        {
            ProductRepository.UnpurchaseProduct(id);
            return RedirectToAction(nameof(Purchases));
        }

        public IActionResult Purchases()
        {
            var products = ProductRepository.GetPurchasedProducts();
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

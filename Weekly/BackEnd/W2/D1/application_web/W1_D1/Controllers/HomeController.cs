using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using W2_D1.Models;
using System.Collections.Generic;
using System.Linq;

namespace W2_D1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private static List<FoodItem> Menu = new List<FoodItem>
        {
            new FoodItem { Id = 1, Name = "Coca Cola 150 ml", Price = 2.50m },
            new FoodItem { Id = 2, Name = "Insalata di pollo", Price = 5.20m },
            new FoodItem { Id = 3, Name = "Pizza Margherita", Price = 10.00m },
            new FoodItem { Id = 4, Name = "Pizza 4 Formaggi", Price = 12.50m },
            new FoodItem { Id = 5, Name = "Pz patatine fritte", Price = 3.50m },
            new FoodItem { Id = 6, Name = "Insalata di riso", Price = 8.00m },
            new FoodItem { Id = 7, Name = "Frutta di stagione", Price = 5.00m },
            new FoodItem { Id = 8, Name = "Pizza fritta", Price = 5.00m },
            new FoodItem { Id = 9, Name = "Piadina vegetariana", Price = 6.00m },
            new FoodItem { Id = 10, Name = "Panino Hamburger", Price = 7.90m }
        };

        private static List<FoodItem> SelectedItems = new List<FoodItem>();

        public IActionResult Index()
        {
            ViewBag.SelectedItems = SelectedItems;
            return View(Menu);
        }

        [HttpPost]
        public IActionResult AddToOrder(int id)
        {
            var item = SelectedItems.FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                var menuItem = Menu.FirstOrDefault(m => m.Id == id);
                if (menuItem != null)
                {
                    SelectedItems.Add(new FoodItem
                    {
                        Id = menuItem.Id,
                        Name = menuItem.Name,
                        Price = menuItem.Price,
                        Quantity = 1
                    });
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromOrder(int id)
        {
            var item = SelectedItems.FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                {
                    SelectedItems.Remove(item);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult FinalBill()
        {
            decimal total = SelectedItems.Sum(i => i.Price * i.Quantity) + 3.00m; // aggiungi il servizio al tavolo
            ViewBag.Total = total;
            return View(SelectedItems);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

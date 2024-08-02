using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzacode.Data;
using Pizzacode.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzacode.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Login()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Pizza)
                .Where(o => !o.IsCompleted)
                .ToListAsync();

            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsCompleted(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            if (order.IsCompleted)
            {
                ModelState.AddModelError(string.Empty, "L'ordine è già stato completato.");
                return RedirectToAction(nameof(Login));
            }

            order.IsCompleted = true;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Login));
        }


        public async Task<IActionResult> DailyReport(DateTime? date)
        {
            if (date == null)
            {
                return View();
            }

            var selectedDate = date.Value.Date;

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Pizza)
                .Where(o => o.CreatedAt.Date == selectedDate && o.IsCompleted)
                .ToListAsync();

            var totalOrders = orders.Count;
            var totalRevenue = orders.Sum(o => o.OrderItems.Sum(oi => oi.Quantity * oi.Pizza.Price));

            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalRevenue = totalRevenue;

            return View();
        }

    }
}

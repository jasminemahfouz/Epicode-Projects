using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzacode.Data;
using Pizzacode.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzacode.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Completa l'ordine
        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int orderId, string address, string notes)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                ModelState.AddModelError(string.Empty, "Address is required.");
                return RedirectToAction("OrderSummary", new { orderId = orderId });
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            order.Address = address;
            order.Notes = notes;
            order.IsCompleted = true;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmed");
        }

        // Visualizza la conferma dell'ordine
        public IActionResult OrderConfirmed()
        {
            return View();
        }

        // OrderController.cs
        public async Task<IActionResult> MyOrders()
        {
            var username = User.Identity.Name;
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Pizza)
                .Where(o => o.UserId == user.Id)
                .ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Pizza)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


    }
}

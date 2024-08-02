using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzacode.Data;
using Pizzacode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzacode.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PizzaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Visualizza tutte le pizze
        public async Task<IActionResult> Index()
        {
            var pizzas = await _context.Pizzas.ToListAsync();
            return View(pizzas);
        }

        // Ordina una pizza
        [HttpPost]
        public async Task<IActionResult> Order(int pizzaId, int quantity)
        {
            var pizza = await _context.Pizzas.FindAsync(pizzaId);
            if (pizza == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }

            var order = new Order
            {
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false,
                Address = string.Empty, // Temp address, will be filled later
                Notes = string.Empty,   // Temp notes, will be filled later
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { PizzaId = pizza.Id, Quantity = quantity }
                }
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderSummary", new { orderId = order.Id });
        }

        // Riepilogo dell'ordine
        public async Task<IActionResult> OrderSummary(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Pizza)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            return View("OrderSummary", order);
        }

        // Crea una nuova pizza (solo per admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // Salva una nuova pizza (solo per admin)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ImageUrl,Price,DeliveryTime,Ingredients")] Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }

        // Modifica una pizza (solo per admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        // Salva modifiche a una pizza (solo per admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl,Price,DeliveryTime,Ingredients")] Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaExists(pizza.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }

        // Elimina una pizza (solo per admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizzas.Any(e => e.Id == id);
        }
    }
}

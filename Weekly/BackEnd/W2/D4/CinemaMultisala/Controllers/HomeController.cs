using Microsoft.AspNetCore.Mvc;
using CinemaMultisala.Models;
using System.Collections.Generic;
using System.Linq;

namespace CinemaMultisala.Controllers
{
    public class HomeController : Controller
    {
        private static List<Sala> Sale = new List<Sala>
        {
            new Sala { Nome = "SALA NORD" },
            new Sala { Nome = "SALA EST" },
            new Sala { Nome = "SALA SUD" }
        };

        public IActionResult Index()
        {
            return View(Sale);
        }

        [HttpPost]
        public IActionResult Vendi(Biglietto biglietto)
        {
            var sala = Sale.FirstOrDefault(s => s.Nome == biglietto.Sala);
            if (sala != null && sala.BigliettiVenduti < sala.Capienza)
            {
                sala.Biglietti.Add(biglietto);
                sala.BigliettiVenduti++;
                if (biglietto.TipoBiglietto == "Ridotto")
                {
                    sala.BigliettiRidotti++;
                }
            }
            return RedirectToAction("Index");
        }
    }
}

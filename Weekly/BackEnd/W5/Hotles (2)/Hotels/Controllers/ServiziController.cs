using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hotels.DAO;
using Hotels.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hotels.Controllers
{
    [Authorize(Policy = "Admin")]
    public class ServiziController : Controller
    {
        private readonly IServizioDao _servizioDao;

        public ServiziController(IServizioDao servizioDao)
        {
            _servizioDao = servizioDao;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var servizi = await _servizioDao.GetAllAsync();
                return View(servizi);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var servizio = await _servizioDao.GetByIdAsync(id);
            if (servizio == null)
            {
                return NotFound();
            }
            return View(servizio);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Servizio servizio)
        {
            if (ModelState.IsValid)
            {
                await _servizioDao.AddAsync(servizio);
                return RedirectToAction(nameof(Index));
            }
            return View(servizio);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var servizio = await _servizioDao.GetByIdAsync(id);
            if (servizio == null)
            {
                return NotFound();
            }
            return View(servizio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servizio servizio)
        {
            if (id != servizio.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _servizioDao.UpdateAsync(servizio);
                return RedirectToAction(nameof(Index));
            }
            return View(servizio);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _servizioDao.DeleteAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}

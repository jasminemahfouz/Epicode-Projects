using Hotels.DAO;
using Hotels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hotels.Controllers
{
    [Authorize(Policy = "Admin")]
    public class CamereController : Controller
    {
        private readonly ICameraDao _cameraDao;

        public CamereController(ICameraDao cameraDao)
        {
            _cameraDao = cameraDao;
        }

        public async Task<IActionResult> Index()
        {
            var camere = await _cameraDao.GetAllAsync();
            return View(camere);
        }

        public async Task<IActionResult> Details(int id)
        {
            var camera = await _cameraDao.GetByIdAsync(id);
            if (camera == null)
            {
                return NotFound();
            }
            return View(camera);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Camera camera)
        {
            if (ModelState.IsValid)
            {
                await _cameraDao.AddAsync(camera);
                return RedirectToAction(nameof(Index));
            }
            return View(camera);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var camera = await _cameraDao.GetByIdAsync(id);
            if (camera == null)
            {
                return NotFound();
            }
            return View(camera);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Camera camera)
        {
            if (id != camera.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _cameraDao.UpdateAsync(camera);
                return RedirectToAction(nameof(Index));
            }
            return View(camera);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _cameraDao.DeleteAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}

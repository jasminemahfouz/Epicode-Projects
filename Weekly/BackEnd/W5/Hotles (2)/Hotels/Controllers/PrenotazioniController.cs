using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Hotels.DAO;
using Hotels.Models;
using System.Threading.Tasks;

namespace Hotels.Controllers
{
    [Authorize(Policy = "Admin")]
    public class PrenotazioniController : Controller
    {
        private readonly IPrenotazioneDao _prenotazioneDao;
        private readonly IClienteDao _clienteDao;
        private readonly ICameraDao _cameraDao;
        private readonly IServizioDao _servizioDao;

        public PrenotazioniController(IPrenotazioneDao prenotazioneDao, IClienteDao clienteDao, ICameraDao cameraDao, IServizioDao servizioDao)
        {
            _prenotazioneDao = prenotazioneDao;
            _clienteDao = clienteDao;
            _cameraDao = cameraDao;
            _servizioDao = servizioDao;
        }

        public async Task<IActionResult> Index()
        {
            var prenotazioni = await _prenotazioneDao.GetAllAsync();
            return View(prenotazioni);
        }

        public async Task<IActionResult> Details(int id)
        {
            var prenotazione = await _prenotazioneDao.GetByIdAsync(id);
            if (prenotazione == null)
            {
                return NotFound();
            }
            prenotazione.Servizi = (await _servizioDao.GetByPrenotazioneIdAsync(id)).ToList();
            return View(prenotazione);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Clienti = await _clienteDao.GetAllAsync();
            ViewBag.Camere = await _cameraDao.GetAllAsync();
            ViewBag.Servizi = await _servizioDao.GetAllAsync();

            var prenotazione = new Prenotazione
            {
                Anno = 2024,
                NumeroProgressivo = await _prenotazioneDao.GetLastIdAsync() + 1
            };

            return View(prenotazione);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prenotazione prenotazione)
        {
            if (ModelState.IsValid)
            {
                await _prenotazioneDao.AddAsync(prenotazione);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clienti = await _clienteDao.GetAllAsync();
            ViewBag.Camere = await _cameraDao.GetAllAsync();
            ViewBag.Servizi = await _servizioDao.GetAllAsync();
            return View(prenotazione);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var prenotazione = await _prenotazioneDao.GetByIdAsync(id);
            if (prenotazione == null)
            {
                return NotFound();
            }
            prenotazione.ServiziSelezionati = (await _servizioDao.GetByPrenotazioneIdAsync(id)).Select(s => s.Id).ToList();

            ViewBag.Clienti = await _clienteDao.GetAllAsync();
            ViewBag.Camere = await _cameraDao.GetAllAsync();
            ViewBag.Servizi = await _servizioDao.GetAllAsync();
            return View(prenotazione);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Prenotazione prenotazione)
        {
            if (id != prenotazione.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _prenotazioneDao.UpdateAsync(prenotazione);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clienti = await _clienteDao.GetAllAsync();
            ViewBag.Camere = await _cameraDao.GetAllAsync();
            ViewBag.Servizi = await _servizioDao.GetAllAsync();
            return View(prenotazione);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _prenotazioneDao.DeleteAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCameraPrice(int id)
        {
            var camera = await _cameraDao.GetByIdAsync(id);
            if (camera == null)
            {
                return NotFound();
            }
            return Json(camera.Prezzo);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(int id)
        {
            var prenotazione = await _prenotazioneDao.GetByIdAsync(id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            prenotazione.Servizi = (await _servizioDao.GetByPrenotazioneIdAsync(id)).ToList();

            var giorniSoggiorno = (prenotazione.Al - prenotazione.Dal).Days;
            var totaleStanza = prenotazione.Tariffa * giorniSoggiorno;
            var totaleServizi = prenotazione.Servizi.Sum(s => s.Prezzo);
            var totale = totaleStanza + totaleServizi - prenotazione.Caparra;

            var viewModel = new CheckoutViewModel
            {
                Prenotazione = prenotazione,
                TotaleStanza = totaleStanza,
                TotaleServizi = totaleServizi,
                Totale = totale
            };

            return View("Checkout", viewModel);
        }

        public async Task<IActionResult> DownloadPdf(int id)
        {
            var prenotazione = await _prenotazioneDao.GetByIdAsync(id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            prenotazione.Servizi = (await _servizioDao.GetByPrenotazioneIdAsync(id)).ToList();

            var checkoutViewModel = new CheckoutViewModel
            {
                Prenotazione = prenotazione,
                TotaleStanza = (prenotazione.Al - prenotazione.Dal).Days * prenotazione.Tariffa,
                TotaleServizi = prenotazione.Servizi.Sum(s => s.Prezzo),
                Totale = ((prenotazione.Al - prenotazione.Dal).Days * prenotazione.Tariffa) + prenotazione.Servizi.Sum(s => s.Prezzo) - prenotazione.Caparra
            };

            using (var ms = new MemoryStream())
            {
                var document = new Document();
                PdfWriter.GetInstance(document, ms);
                document.Open();

                document.Add(new Paragraph("Riepilogo Check-out"));
                document.Add(new Paragraph($"Cliente: {checkoutViewModel.Prenotazione.Cliente.Cognome} {checkoutViewModel.Prenotazione.Cliente.Nome}"));
                document.Add(new Paragraph($"Camera: {checkoutViewModel.Prenotazione.Camera.Descrizione}"));
                document.Add(new Paragraph($"Dal: {checkoutViewModel.Prenotazione.Dal.ToShortDateString()}"));
                document.Add(new Paragraph($"Al: {checkoutViewModel.Prenotazione.Al.ToShortDateString()}"));
                document.Add(new Paragraph($"Tariffa giornaliera: {checkoutViewModel.Prenotazione.Tariffa.ToString("C")}"));
                document.Add(new Paragraph($"Caparra: {checkoutViewModel.Prenotazione.Caparra.ToString("C")}"));

                document.Add(new Paragraph("Servizi Aggiuntivi:"));
                foreach (var servizio in checkoutViewModel.Prenotazione.Servizi)
                {
                    document.Add(new Paragraph($"{servizio.Descrizione} - {servizio.Prezzo.ToString("C")}"));
                }

                document.Add(new Paragraph($"Totale Stanza: {checkoutViewModel.TotaleStanza.ToString("C")}"));
                document.Add(new Paragraph($"Totale Servizi Aggiuntivi: {checkoutViewModel.TotaleServizi.ToString("C")}"));
                document.Add(new Paragraph($"Caparra Iniziale: {checkoutViewModel.Prenotazione.Caparra.ToString("C")}"));
                document.Add(new Paragraph($"Totale Da Saldare: {checkoutViewModel.Totale.ToString("C")}"));

                document.Close();

                return File(ms.ToArray(), "application/pdf", "Riepilogo_Checkout.pdf");
            }
        }
    }
}

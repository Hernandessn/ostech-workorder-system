using Microsoft.AspNetCore.Mvc;
using OSTech.WebMVC.Models;
using OSTech.WebMVC.Services;

namespace OSTech.WebMVC.Controllers
{
    public class TechniciansController : Controller
    {
        private readonly ITechnicianService _technicianService;
        public TechniciansController(ITechnicianService technicianService)
        {
            _technicianService = technicianService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TechnicianViewModel>>> Index()
        {
            var result = await _technicianService.GetTechniciansAsync();

            if (result is null)
                return View("Error");

            return View(result);
        }
        [HttpGet]
        public IActionResult CreateNewTechnician()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<TechnicianViewModel>> CreateNewTechnician(TechnicianViewModel technicianVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _technicianService.CreateTechnician(technicianVM);

                if(result !=  null)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Error creating technician";
            return View(technicianVM);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTechnician(int id)
        {
            var result = await _technicianService.GetTechnicianByIdAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }
        [HttpPost]
        public async Task<ActionResult<TechnicianViewModel>> UpdateTechnician(int id, TechnicianViewModel technicianVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _technicianService.UpdateTechnicianAsync(id, technicianVM);

                if (result)
                    return RedirectToAction(nameof(Index));
            }

            ViewBag.Erro = "Error updating technician";
            return View(technicianVM);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteTechnician(int id)
        {
            var result = await _technicianService.GetTechnicianByIdAsync(id);

            if (result == null)
                return View("Error");

            return View(result);
        }

        [HttpPost, ActionName("DeleteTechnician")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _technicianService.DeleteTechnician(id);
            if (result)
                return RedirectToAction("Index");

            return View(result);
        }
    }
}

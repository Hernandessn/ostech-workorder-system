using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OSTech.WebMVC.Models;
using OSTech.WebMVC.Services;

namespace OSTech.WebMVC.Controllers
{
    public class EquipmentsController : Controller
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentsController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentViewModel>>> Index()
        {
            var result = await _equipmentService.GetEquipmentsAsync();

            if (result == null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateNewEquipment()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentViewModel>> CreateNewEquipment(EquipmentViewModel equipmentVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _equipmentService.CreateEquipment(equipmentVM);

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Error creating equipment";
            return View(equipmentVM);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEquipment(int id)
        {
            var result = await _equipmentService.GetEquipmentByIdAsync(id);

            if (result == null)
                return View("Error");

            return View(result);
        }
        [HttpPost]
        public async Task<ActionResult<EquipmentViewModel>> UpdateEquipment(int id,  EquipmentViewModel equipmentVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _equipmentService.UpdateEquipmentAsync(id, equipmentVM);

                if(result)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Error updating equipment";
            return View(equipmentVM);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var result = await _equipmentService.GetEquipmentByIdAsync(id);

            if (result == null)
                return View("Error");

            return View(result);
        }

        [HttpPost, ActionName("DeleteEquipment")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _equipmentService.DeleteEquipment(id);

            if(result) 
                return RedirectToAction("Index");

            return View(result);
        }
    }
}

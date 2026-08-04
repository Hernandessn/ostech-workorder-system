using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSTech.WebMVC.Models;
using OSTech.WebMVC.Services;

namespace OSTech.WebMVC.Controllers
{
    public class WorkOrdersController : Controller
    {
        private readonly IWorkOrderService _workOrderService;
        private readonly ITechnicianService _technicianService;
        private readonly ICustomerService _customerService;
        private readonly ICategoryService _categoryService;
        private readonly IEquipmentService _equipmentService;


        public WorkOrdersController(IWorkOrderService workOrderService, 
            ITechnicianService technicianService, ICustomerService customerService, 
            ICategoryService categoryService, IEquipmentService equipmentService)
        {
            _workOrderService = workOrderService;
            _technicianService = technicianService;
            _customerService = customerService;
            _categoryService = categoryService;
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOrderViewModel>>> Index()
        {
            var result = await _workOrderService.GetWorkOrdersAsync();

            if (result is null)
                return View("Error");

            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> CreateNewWorkOrder()
        {
            var model = new WorkOrderViewModel();

            model.Technicians = (await _technicianService.GetTechniciansAsync())
                .Select(x => new SelectListItem
                {
                    Value = x.TechnicianId.ToString(),
                    Text = x.Name
                });

            model.Customers = (await _customerService.GetCustomersAsync())
                .Select(x => new SelectListItem
                {
                    Value = x.CustomerId.ToString(),
                    Text = x.Name
                });

            model.Categories = (await _categoryService.GetCategoriesAsync())
                .Select(x => new SelectListItem
                {
                    Value = x.CategoryId.ToString(),
                    Text = x.Name
                });

            model.Equipments = (await _equipmentService.GetEquipmentsAsync())
                .Select(x => new SelectListItem
                {
                    Value = x.EquipmentId.ToString(),
                    Text = x.Name
                });

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult<WorkOrderViewModel>> CreateNewWorkOrder(WorkOrderViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _workOrderService.CreateWorkOrder(categoryVM);

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Error creating category";
            return View(categoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateWorkOrder(int id)
        {
            var model = await _workOrderService.GetWorkOrderByIdAsync(id);

            if (model == null)
                return View("Error");

            model.Technicians = (await _technicianService.GetTechniciansAsync())
                .Select(x => new SelectListItem
                {
                    Value = x.TechnicianId.ToString(),
                    Text = x.Name
                });

            model.Customers = (await _customerService.GetCustomersAsync())
                .Select(x => new SelectListItem
                {
                    Value = x.CustomerId.ToString(),
                    Text = x.Name
                });

            model.Categories = (await _categoryService.GetCategoriesAsync())
                .Select(x => new SelectListItem
                {
                    Value = x.CategoryId.ToString(),
                    Text = x.Name
                });

            model.Equipments = (await _equipmentService.GetEquipmentsAsync())
                .Select(x => new SelectListItem
                {
                    Value = x.EquipmentId.ToString(),
                    Text = x.Name
                });

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult<WorkOrderViewModel>> UpdateWorkOrder(int id, WorkOrderViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _workOrderService.UpdateWorkOrderAsync(id, categoryVM);

                if (result)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Error updating category";
            return View(categoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteWorkOrder(int id)
        {
            var result = await _workOrderService.GetWorkOrderByIdAsync(id);

            if (result == null)
                return View("Error");

            return View(result);
        }

        [HttpPost, ActionName("DeleteWorkOrder")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _workOrderService.DeleteWorkOrder(id);
            if (result)
                return RedirectToAction("Index");

            return View(result);
        }
    }
}

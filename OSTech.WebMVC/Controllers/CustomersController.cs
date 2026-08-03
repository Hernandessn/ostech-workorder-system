using Microsoft.AspNetCore.Mvc;
using OSTech.WebMVC.Models;
using OSTech.WebMVC.Services;

namespace OSTech.WebMVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> Index()
        {
            var result = await _customerService.GetCustomersAsync();

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateNewCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> CreateNewCustomer(CustomerViewModel customerVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerService.CreateCustomer(customerVM);

                if(result !=  null)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Error creating customer";
            return View(customerVM);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var result = await _customerService.GetCustomerByIdAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> UpdateCustomer(int id, CustomerViewModel customerVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerService.UpdateCustomer(id, customerVM);
                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Error updating customer";
            return View(customerVM);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.GetCustomerByIdAsync(id);

            if (result == null)
                return View("Error");

            return View(result);
        }

        [HttpPost, ActionName("DeleteCustomer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _customerService.DeleteCustomer(id);
            if (result)
                return RedirectToAction("Index");

            return View(result);
        }
    }
}

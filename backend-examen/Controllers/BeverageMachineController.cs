using Microsoft.AspNetCore.Mvc;
using backend_examen.Application.Services;
using backend_examen.Models;

namespace backend_examen.Controllers
{
    [ApiController]
    [Route("api/beveragemachine")]
    public class BeverageMachineController : ControllerBase
    {
        private readonly VendingMachineService _vendingService;

        public BeverageMachineController(VendingMachineService vendingService)
        {
            _vendingService = vendingService;
        }

        [HttpGet("products")]
        public ActionResult<List<DrinksModel>> GetProducts()
        {
            return _vendingService.GetAvailableDrinks();
        }

       [HttpPost("buy/{id}")]
        public IActionResult BuyDrink(int id)
        {
            var result = _vendingService.BuyDrink(id);
            if (!result)
                return NotFound("Refresco no encontrado o agotado.");

            return Ok();
        }
    }
}

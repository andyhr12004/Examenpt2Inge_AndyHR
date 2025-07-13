using Microsoft.AspNetCore.Mvc;
using backend_examen.Application;           
using backend_examen.Application.DTOs;     
using backend_examen.Domain;              
using System.Collections.Generic;

namespace backend_examen.API
{
    [ApiController]
    [Route("api/vendingmachine")]
    public class BeverageMachineController : ControllerBase
    {
        private readonly GetBeveragesQuery _getQuery;
        private readonly BuyBeverageCommand _buyCommand;

        public BeverageMachineController(
            GetBeveragesQuery getQuery,
            BuyBeverageCommand buyCommand)
        {
            _getQuery   = getQuery;
            _buyCommand = buyCommand;
        }

        [HttpGet("products")]
        public ActionResult<List<DrinksModel>> GetProducts()
        {
            var drinks = _getQuery.Execute();
            return Ok(drinks);
        }

        [HttpPost("buy")]
        public ActionResult<BuyProductsResponseDto> BuyDrink([FromBody] BuyRequestDto dto)
        {
            var result = _buyCommand.Execute(dto.DrinkId, dto.Quantity);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Contracts;
using Models.Models;
using Services;

namespace ToysAndGames.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //TODO: We need to catch errros
        private readonly IToysAndGamesService _toysAndGamesService;

        public ProductsController(IToysAndGamesService toysAndGamesService)
        {
            _toysAndGamesService = toysAndGamesService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_toysAndGamesService.GetAllProducts());
        }

        [HttpPost]
        public IActionResult NewProduct([FromBody] ProductDto product)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                if (_toysAndGamesService.CreateProduct(product))
                    return StatusCode(StatusCodes.Status201Created);
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                //LOGS
            }
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody] ProductDto product)
        {
            if(!ModelState.IsValid) return BadRequest();

            if (_toysAndGamesService.UpdateProduct(product))
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public IActionResult DeleteProduct([FromQuery] int? id)
        {
            //TODO: Lets move this to a nullable int.
            if (id==null) return BadRequest();
            if(_toysAndGamesService.DeleteProduct(id.Value))
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

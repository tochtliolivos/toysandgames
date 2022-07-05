using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Unosquare.ToysAndGames.Models.Contracts;
using Unosquare.ToysAndGames.Models.Dtos;

namespace UnosquareToysAndGames.API.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IToysAndGamesService _toysAndGamesService;
        private readonly ILogger<ProductsController> _logger;
        public readonly IValidator<ProductDto> _dtoValidator;

        public ProductsController(IToysAndGamesService toysAndGamesService, ILogger<ProductsController> logger, 
            IValidator<ProductDto> validator)
        {
            _toysAndGamesService = toysAndGamesService;
            _logger = logger;
            _dtoValidator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Ok(await _toysAndGamesService.GetAllProducts());
            }
            catch(Exception e)
            {
                _logger.LogError($"Error getting products: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> NewProduct([FromBody] ProductDto product)
        {
            try
            {
                if (!_dtoValidator.Validate(product).IsValid) return BadRequest();
                var result = await _toysAndGamesService.CreateProduct(product);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating product: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto product)
        {
            try
            {
                if (!_dtoValidator.Validate(product).IsValid) return BadRequest();
                var result = await _toysAndGamesService.UpdateProduct(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }                        
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                var result = await _toysAndGamesService.DeleteProduct(id.Value);
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error deleting product: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using OrderManagement.Services;

namespace OrderManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService service, ILogger<ProductsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Request: Get all products");
            var result = await _service.GetAllAsync();
            _logger.LogInformation("Returned {Count} products", result.Count());
            return Ok(result);
        }
    }
}

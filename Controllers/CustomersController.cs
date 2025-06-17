using Microsoft.AspNetCore.Mvc;
using OrderManagement.DTOs;
using OrderManagement.Services;

namespace OrderManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly ILogger<CustomersController> _logger;
        public CustomersController(ICustomerService service, ILogger<CustomersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Request: Get all customers");
            var result = await _service.GetAllAsync();
            _logger.LogInformation("Returned {Count} customers", result.Count());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Request: Get customer by id {CustomerId}", id);
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                _logger.LogWarning("Customer {CustomerId} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Returned customer {CustomerId}", id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDto dto)
        {
            _logger.LogInformation("Request: Create new customer");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create customer failed: invalid model");
                return BadRequest(ModelState);
            }
            var result = await _service.AddAsync(dto);
            _logger.LogInformation("Created customer {CustomerId}", result.CustomerId);
            return CreatedAtAction(nameof(GetById), new { id = result.CustomerId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerUpdateDto dto)
        {
            _logger.LogInformation("Request: Update customer {CustomerId}", id);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

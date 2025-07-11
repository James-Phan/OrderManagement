using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.DTOs;
using OrderManagement.Models;
using OrderManagement.Repositories;

namespace OrderManagement.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(ICustomerRepository repo, ILogger<CustomerService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            return customers.Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            });
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return null;
            return new CustomerDto
            {
                CustomerId = c.CustomerId,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            };
        }

        public async Task<CustomerDto> AddAsync(CustomerCreateDto dto)
        {
            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };
            await _repo.AddAsync(customer);
            _logger.LogInformation("Created new customer: {CustomerId} - {FullName}", customer.CustomerId, customer.FullName);
            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public async Task<bool> UpdateAsync(int id, CustomerUpdateDto dto)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null)
            {
                _logger.LogWarning("Update failed: Customer {CustomerId} not found", id);
                return false;
            }
            customer.FullName = dto.FullName;
            customer.Email = dto.Email;
            customer.PhoneNumber = dto.PhoneNumber;
            await _repo.UpdateAsync(customer);
            _logger.LogInformation("Updated customer: {CustomerId}", id);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exists = await _repo.ExistsAsync(id);
            if (!exists)
            {
                _logger.LogWarning("Delete failed: Customer {CustomerId} not found", id);
                return false;
            }
            await _repo.DeleteAsync(id);
            _logger.LogInformation("Deleted customer: {CustomerId}", id);
            return true;
        }
    }
}

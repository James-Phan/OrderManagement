using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.DTOs;

namespace OrderManagement.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(int id);
        Task<CustomerDto> AddAsync(CustomerCreateDto dto);
        Task<bool> UpdateAsync(int id, CustomerUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

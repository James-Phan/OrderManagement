using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.DTOs;

namespace OrderManagement.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
    }
}

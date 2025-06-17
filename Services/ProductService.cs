using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.DTOs;
using OrderManagement.Repositories;

namespace OrderManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repo.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price
            });
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;
            return new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.Models;

namespace OrderManagement.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync(DateTime? fromDate = null, DateTime? toDate = null, int? customerId = null);
        Task<Order> GetByIdAsync(int id);
        Task<Order> AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}

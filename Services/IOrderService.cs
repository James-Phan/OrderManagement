using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.DTOs;

namespace OrderManagement.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync(DateTime? fromDate = null, DateTime? toDate = null, int? customerId = null);
        Task<OrderDetailDto> GetByIdAsync(int id);
        Task<OrderDetailDto> AddAsync(OrderCreateDto dto);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderManagement.DTOs;
using OrderManagement.Models;
using OrderManagement.Repositories;

namespace OrderManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IProductRepository _productRepo;
        public OrderService(IOrderRepository orderRepo, ICustomerRepository customerRepo, IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
            _productRepo = productRepo;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync(DateTime? fromDate = null, DateTime? toDate = null, int? customerId = null)
        {
            var orders = await _orderRepo.GetAllAsync(fromDate, toDate, customerId);
            var list = new List<OrderDto>();
            foreach (var o in orders)
            {
                list.Add(new OrderDto
                {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer?.FullName,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    OrderItems = o.OrderItems?.Select(oi => new OrderItemDto
                    {
                        OrderItemId = oi.OrderItemId,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product?.Name,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList() ?? new List<OrderItemDto>()
                });
            }
            return list;
        }

        public async Task<OrderDetailDto> GetByIdAsync(int id)
        {
            var o = await _orderRepo.GetByIdAsync(id);
            if (o == null) return null;
            return new OrderDetailDto
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer?.FullName,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                OrderItems = o.OrderItems?.Select(oi => new OrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList() ?? new List<OrderItemDto>()
            };
        }

        public async Task<OrderDetailDto> AddAsync(OrderCreateDto dto)
        {
            // Validate customer
            var customer = await _customerRepo.GetByIdAsync(dto.CustomerId);
            if (customer == null) return null;

            // Validate products & tính tổng tiền
            decimal total = 0;
            var orderItems = new List<OrderItem>();
            foreach (var item in dto.OrderItems)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                if (product == null) return null;
                var orderItem = new OrderItem
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };
                total += product.Price * item.Quantity;
                orderItems.Add(orderItem);
            }

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = dto.OrderDate,
                TotalAmount = total,
                OrderItems = orderItems
            };
            await _orderRepo.AddAsync(order);

            return await GetByIdAsync(order.OrderId);
        }
    }
}

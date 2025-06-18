using System.Threading.Tasks;
using OrderManagement.DTOs;
using OrderManagement.Models;
using OrderManagement.Repositories;
using OrderManagement.Services;
using Moq;
using Microsoft.Extensions.Logging;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagement.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task AddAsync_ShouldReturnCreatedCustomerDto()
        {
            // Arrange
            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.AddAsync(It.IsAny<Customer>())).ReturnsAsync((Customer c) => { c.CustomerId = 1; return c; });
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var service = new CustomerService(repoMock.Object, mockLogger.Object);
            var dto = new CustomerCreateDto { FullName = "Test", Email = "test@email.com", PhoneNumber = "0123456789" };

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.FullName);
            Assert.Equal("test@email.com", result.Email);
            Assert.Equal("0123456789", result.PhoneNumber);
            Assert.Equal(1, result.CustomerId);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfCustomerDto()
        {
            // Arrange
            var customers = new List<Customer> {
                new Customer { CustomerId = 1, FullName = "A", Email = "a@email.com", PhoneNumber = "111" },
                new Customer { CustomerId = 2, FullName = "B", Email = "b@email.com", PhoneNumber = "222" }
            };
            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(customers);
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var service = new CustomerService(repoMock.Object, mockLogger.Object);

            // Act
            var result = (await service.GetAllAsync()).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("A", result[0].FullName);
            Assert.Equal("B", result[1].FullName);
        }
    }
}

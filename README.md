# OrderManagement - .NET 8 Web API

## Mô tả
Hệ thống quản lý đơn hàng (Order Management System) sử dụng ASP.NET Core 8, Entity Framework Core 8, SQL Server. Hỗ trợ các chức năng CRUD cho Customer, Product, Order, OrderItem. API chuẩn RESTful, có Swagger UI để test.

## Cấu trúc thư mục
- `Controllers/` : Các controller API
- `Models/` : Entity models & DbContext
- `DTOs/` : Data Transfer Objects
- `Repositories/` : Repository pattern
- `Services/` : Business logic

## Hướng dẫn cài đặt & chạy
1. **Clone hoặc giải nén source code**
2. **Cài đặt .NET 8 SDK**: https://dotnet.microsoft.com/download/dotnet/8.0
3. **Cài đặt SQL Server LocalDB hoặc SQL Server Express**
4. **Cấu hình chuỗi kết nối** trong `appsettings.json` (ví dụ):
   ```json
   "DefaultConnection": "Server=.\\SQL2022;Database=OrderManagementDb;User=sa;Password=123;MultipleActiveResultSets=true;TrustServerCertificate=True"
   ```
5. **Chạy migration để tạo database:**
   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef database update
   ```
6. **Chạy ứng dụng:**
   ```bash
   dotnet run
   ```
7. **Mở trình duyệt truy cập Swagger UI:**
   - `https://localhost:5001/swagger` hoặc `http://localhost:5000/swagger`

## Logging
The project uses ASP.NET Core built-in logging (`ILogger<T>`) for all services and controllers.

- **Where logs are written:**
  - By default, logs are output to the console.
  - You can configure output (console, file, etc.) in `appsettings.json` under the `Logging` section.
- **What is logged:**
  - All important CRUD operations on Customers, Orders, Products (create, update, delete, get, errors...)
  - Warnings for invalid input or not found cases.
- **How to change log level:**
  - Edit the `Logging` section in `appsettings.json`:
    ```json
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning",
        "OrderManagement": "Information"
      }
    }
    ```
- **Sample log in code:**
    ```csharp
    _logger.LogInformation("Created customer {CustomerId}", result.CustomerId);
    _logger.LogWarning("Delete customer failed: customer {CustomerId} not found", id);
    ```

## Test API
- Sử dụng Swagger UI hoặc Postman để test các endpoint
- Các endpoint chính:
  - `GET /api/customers`, `POST /api/customers`, `PUT /api/customers/{id}`, `DELETE /api/customers/{id}`
  - `GET /api/products`
  - `POST /api/orders`, `GET /api/orders`, `GET /api/orders/{id}`

## Unit Test
- Đã chuẩn bị sẵn project test (OrderManagement.Tests) với 1-2 test mẫu cho Service.
- Chạy test:
  ```bash
  dotnet test
  ```

## (Optional) Postman collection
- Có thể xuất collection từ Swagger UI hoặc Postman nếu cần gửi kèm.

---

Nếu gặp vấn đề về migration/database, hãy kiểm tra lại connection string và quyền truy cập SQL Server.

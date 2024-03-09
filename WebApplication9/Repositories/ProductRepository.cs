using System.Data.Common;
using Dapper;
using WebApplication9.Domain;
using WebApplication9.DTO;
using WebApplication9.Migrations;
using WebApplication9.Repositories.Interfaces;

namespace WebApplication9.Repositories;

public class ProductRepository: IProductRepository
{
    private ConnectionDatabase _connection;

    public ProductRepository(ConnectionDatabase connection)
    {
        _connection = connection;
    }

    public async Task<List<Product>> GetAll()
    {
        using (var connection = _connection.CreateConnection())
        {
            return (await connection.QueryAsync<Product>("SELECT * FROM products")).ToList();
        }
    }

    public async Task<Product> GetProduct(int id)
    {
        using (var connection = _connection.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM product WHERE id = @id");
        }
    }

    public async void Add(ProductDTO product)
    {
        using (var connection = _connection.CreateConnection())
        {
            await connection.QueryAsync(
                "INSERT INTO products (name, description, category, price) VALUES (@name, @description, @category, @price)");
        }
    }

    public async void Update(int id, ProductDTO productDto)
    {
        using (var connection = _connection.CreateConnection())
        {
            await connection.QueryAsync("UPDATE products SET name = @name, description = @description, category = @category, price = @price WHERE id = @id");
        }
    }

    public async void Delete(int id)
    {
        using (var connection = _connection.CreateConnection())
        {
            await connection.QueryAsync("DELETE FROM products WHERE id = @id", new { id });
        }
    }
}
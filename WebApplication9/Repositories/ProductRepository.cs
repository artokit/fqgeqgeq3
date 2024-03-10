using Dapper;
using WebApplication9.Domain;
using WebApplication9.DTO;
using WebApplication9.Models;
using WebApplication9.Repositories.Interfaces;

namespace WebApplication9.Repositories;

public class ProductRepository: IProductRepository
{
    private ConnectionDatabase _connection;

    public ProductRepository(ConnectionDatabase connection)
    {
        _connection = connection;
    }

    public async Task<List<Product>?> GetAll(int offset, int count)
    {
        using (var connection = _connection.CreateConnection())
        {
            var res = (await connection.QueryAsync<Product>("SELECT * FROM products ORDER BY id")).ToList();

            if (res.Count - offset < 0)
            {
                return null;
            }
            
            if (res.Count < offset + count)
            {
                return res.Slice(offset, res.Count - offset);
            }
            
            return res.Slice(offset, count);
        }
    }

    public async Task<Product?> GetProduct(int id)
    {
        using (var connection = _connection.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM products WHERE id = @id", id);
        }
    }

    public async void Add(ProductDTO product)
    {
        using (var connection = _connection.CreateConnection())
        {
            await connection.QueryAsync(
                "INSERT INTO products (name, description, category, price) VALUES (@name, @description, @category, @price)", new {product.name, product.description, product.category, product.price});
        }
    }

    public async void Update(int id, ProductDTO productDto)
    {
        using (var connection = _connection.CreateConnection())
        {
            await connection.QueryAsync("UPDATE products SET name = @name, description = @description, category = @category, price = @price WHERE id = @id", productDto);
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
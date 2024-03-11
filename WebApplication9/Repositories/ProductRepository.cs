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
            return await connection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM products WHERE id = @id", new {id});
        }
    }

    public async void Add(CreateProductDTO product)
    {
        using (var connection = _connection.CreateConnection())
        {
            await connection.QueryAsync(
                "INSERT INTO products (name, description, category, price) VALUES (@name, @description, @category, @price)",
                product
            );
        }
    }

    public async void Update(Product product, ProductDTO productDto)
    {
        using (var connection = _connection.CreateConnection())
        {
            await connection.QueryAsync(
                    "UPDATE products SET name = @Name, description = @Description, category = @Category, price = @Price WHERE id = @Id", 
                    new
                    {
                        Name = productDto.Name ?? product.name,
                        Description = productDto.Description ?? product.description,
                        category = productDto.Category ?? product.category,
                        Price = productDto.Price ?? product.price,
                        Id = product.id
                    }
                );
        }
    }

    public async void Delete(int id)
    {
        using (var connection = _connection.CreateConnection())
        {
            await connection.QueryAsync("DELETE FROM products WHERE id = @id", new { id });
        }
    }

    public async Task<bool> AddPhoto(int productId, string fileName)
    {
        using (var connection = _connection.CreateConnection())
        {
            await connection.QueryAsync(
                "UPDATE products SET file_name = @fileName where id = @productId", 
                new {fileName, productId}
            );
            return true;
        }
    }
}
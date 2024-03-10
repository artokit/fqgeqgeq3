using WebApplication9.DTO;
using WebApplication9.Migrations;
using WebApplication9.Models;

namespace WebApplication9.Repositories.Interfaces;

public interface IProductRepository
{
    public Task<List<Product>?> GetAll(int offset, int count);
    public Task<Product?> GetProduct(int id);
    public void Add(ProductDTO product);
    public void Update(int id, ProductDTO productDto);
    public void Delete(int id);
}
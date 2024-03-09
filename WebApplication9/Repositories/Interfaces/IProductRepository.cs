using WebApplication9.DTO;
using WebApplication9.Migrations;

namespace WebApplication9.Repositories.Interfaces;

public interface IProductRepository
{
    public Task<List<Product>> GetAll();
    public Task<Product?> GetProduct(int id);
    public void Add(ProductDTO product);
    public void Update(int id, ProductDTO productDto);
    public void Delete(int id);
}
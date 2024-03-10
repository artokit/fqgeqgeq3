using Microsoft.AspNetCore.Mvc;
using WebApplication9.DTO;
using WebApplication9.Repositories.Interfaces;

namespace WebApplication9.Controllers;

[ApiController]
[Route("product")]
[Produces("application/json")]
public class ProductController(IProductRepository productRepository) : ControllerBase
{
    private IProductRepository _productRepository = productRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll(int offset, int count)
    {
        return Ok(await _productRepository.GetAll(offset, count));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var res = await _productRepository.GetProduct(id);
        return (res is null) ? NotFound() : Ok(res);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(ProductDTO productDto)
    {
        _productRepository.Add(productDto);
        return Ok();
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, ProductDTO productDto)
    {
        if (await _productRepository.GetProduct(id) != null)
        {
            _productRepository.Update(id, productDto);
            return Ok();
        }

        return NotFound();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await _productRepository.GetProduct(id) != null)
        {
            _productRepository.Delete(id);
            return Ok();
        }

        return NotFound();
    }
}
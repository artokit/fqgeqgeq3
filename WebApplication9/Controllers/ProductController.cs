using Microsoft.AspNetCore.Mvc;
using WebApplication9.DTO;
using WebApplication9.Repositories.Interfaces;

namespace WebApplication9.Controllers;

[ApiController]
[Route("product")]
public class ProductController : Controller
{
    private IProductRepository _productRepository;
    private IWebHostEnvironment _apiEnvironment;
    
    public ProductController(IProductRepository productRepository, IWebHostEnvironment apiEnvironment)
    {
        _productRepository = productRepository;
        _apiEnvironment = apiEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int offset, int count)
    {
        if (count > 30)
        {
            return BadRequest("Не дохуя ли ?");
        }
        
        return Ok(await _productRepository.GetAll(offset, count));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var res = await _productRepository.GetProduct(id);
        return (res is null) ? NotFound() : Ok(res);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(CreateProductDTO product)
    {
        _productRepository.Add(product);
        return Ok();
    }

    [HttpPost("upload_photo")]
    public async Task<ActionResult> UploadPhoto(int productId, IFormFile file)
    {
        if (await _productRepository.GetProduct(productId) is null)
        {
            return NotFound();
        }
        
        string path = "/Files/" + file.FileName;
        using (var fileStream = new FileStream(_apiEnvironment.WebRootPath + path, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        
        await _productRepository.AddPhoto(productId, file.FileName);
        return Ok();
        
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, ProductDTO productDto)
    {
        var product = await _productRepository.GetProduct(id);
        
        if (product == null)
        {
            return NotFound();
        }
        
        _productRepository.Update(product, productDto);
        return Ok();
        
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
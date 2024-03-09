using Microsoft.AspNetCore.Mvc;
using WebApplication9.DTO;
using WebApplication9.Services;

namespace WebApplication9.Controllers;

[ApiController]
[Route("/api/store")]
[Produces("application/json")]
public class StoreController : Controller
{
    private StoreService _storeService;
    
    public StoreController(StoreService storeService)
    {
        _storeService = storeService;
    }
        
    [HttpPost]
    [Route("add/{id}")]
    public async Task<ActionResult> AddProductToStore(int id)
    {
        await _storeService.AddProductToStore(HttpContext, id);
        return Ok();
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<StorageDTO>?>> GetAll()
    {
        return Ok(await _storeService.GetAll(HttpContext));
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult> DeleteProductFromStorage(DeleteStorageRequestDTO deleteStorageRequest)
    {
        var res = await _storeService.DeleteFromStorageProduct(HttpContext, deleteStorageRequest.Id);
        return res ? Ok() : NotFound();
    }
}
using WebApplication9.DTO;
using WebApplication9.Repositories.Interfaces;

namespace WebApplication9.Services;

public class StoreService
{
    private IStoreRepository _storeRepository;
    
    public StoreService(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<bool> AddProductToStore(HttpContext context, int prodId)
    {
        string storageUuid = context.Request.Cookies["storage_uuid"] ?? Guid.NewGuid().ToString();
        context.Response.Cookies.Append("storage_uuid", storageUuid);
        await _storeRepository.AddToStorage(storageUuid, prodId);
        return true;
    }

    public async Task<List<StorageDTO>?> GetAll(HttpContext context)
    {
        string? storageUuid = context.Request.Cookies["storage_uuid"];
        
        if (storageUuid is null)
        {
            return null;
        }
        
        return await _storeRepository.GetAll(storageUuid);
    }

    public async Task<bool> DeleteFromStorageProduct(HttpContext context, int productId)
    {
        string? storageUuid = context.Request.Cookies["storage_uuid"];
        
        if (storageUuid is null)
        {
            return false;
        }

        await _storeRepository.DeleteProd(storageUuid, productId);
        
        return true;
    }
}
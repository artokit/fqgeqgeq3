using WebApplication9.DTO;
namespace WebApplication9.Repositories.Interfaces;

public interface IStoreRepository
{
    public Task<bool> AddToStorage(string storageUuid, int prodId);
    public Task<bool> DeleteProd(string storageUuid, int prodId);
    public Task<List<StorageDTO>> GetAll(string storageUuid);
}
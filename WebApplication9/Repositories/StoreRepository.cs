using Dapper;
using WebApplication9.Domain;
using WebApplication9.DTO;
using WebApplication9.Migrations;
using WebApplication9.Repositories.Interfaces;
namespace WebApplication9.Repositories;


public class StoreRepository : IStoreRepository
{
    private ConnectionDatabase _connection;

    public StoreRepository(ConnectionDatabase connection)
    {
        _connection = connection;
    }

    public async Task<bool> AddToStorage(string storageUuid, int prodId)
    {
        using (var connection = _connection.CreateConnection())
        {
            await connection.QueryAsync<Product>(
                "INSERT INTO STORAGES VALUES(@storageUuid, @prodId)", 
                new {storageUuid, prodId}
            );
            return true;
        }
    }

    public async Task<bool> DeleteProd(string storageUuid, int prodId)
    {
        using (var connection = _connection.CreateConnection())
        {
            await connection.QueryAsync("DELETE FROM storages WHERE uuid = @storageUuid AND product_id = @prodId", new {storageUuid, prodId});
            return true;
        }
    }

    public async Task<List<StorageDTO>> GetAll(string storageUuid)
    {
        using (var connection = _connection.CreateConnection())
        {
            return (await connection.QueryAsync<StorageDTO>("SELECT product_id FROM storages where uuid = @storageUuid", new {storageUuid})).ToList();
        }
    }
}
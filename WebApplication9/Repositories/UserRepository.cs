using Dapper;
using WebApplication9.Domain;
using WebApplication9.DTO;
using WebApplication9.Models;
using WebApplication9.Repositories.Interfaces;

namespace WebApplication9.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ConnectionDatabase _connection;

    public UserRepository(ConnectionDatabase connection)
    {
        _connection = connection;
    }
    
    public async Task<User?> AddUser(RegisterDto user)
    {
        using (var connection = _connection.CreateConnection())
        {
            var sql = $"INSERT INTO users(username, password, email) VALUES(@Username, @Password, @Email) RETURNING id";
            var param = new { Username = user.username, Password = user.password, Email = user.email };
            return await connection.QueryFirstOrDefaultAsync<User>(sql, param);
        }
    }

    public async Task<User?> GetUserById(int id)
    {
        using (var connection = _connection.CreateConnection())
        {
            var sql = "SELECT * FROM users WHERE id = @id";
            var param = new { id };
            return await connection.QueryFirstOrDefaultAsync<User>(sql, param);
        }
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        using (var connection = _connection.CreateConnection())
        {
            var sql = "SELECT * FROM users WHERE username = @username";
            var param = new { username };
            return await connection.QueryFirstOrDefaultAsync<User>(sql, param);
        }
    }
    
}
using WebApplication9.DTO;
using WebApplication9.Models;

namespace WebApplication9.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<User?> AddUser(RegisterDto user);
    public Task<User?> GetUserById(int id);
    public Task<User?> GetUserByUsername(string username);
}
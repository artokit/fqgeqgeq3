using WebApplication9.DTO;
using WebApplication9.Models;
using WebApplication9.Repositories.Interfaces;

namespace WebApplication9.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> AddUser(RegisterDto user)
    {
        user.password = Common.Common.HashPassword(user.password);
        return await _userRepository.AddUser(user);
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _userRepository.GetUserByUsername(username);
    }
}
using PigWithAPlan.Server.Dtos.User;
using PigWithAPlan.Server.Interfaces;
using PigWithAPlan.Server.Mappers;
using PigWithAPlan.Server.Models;

public interface IUserService
{
    Task<bool> RegisterAsync(string username, string password);
    Task<bool> ValidateUser(string username, string password);
    Task<List<UserDTO>> GetAllAsync();
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> RegisterAsync(string username, string password)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User
        {
            Name = username,
            Email = username,
            Username = username,
            Password = hashedPassword
        };

        return await _userRepository.CreateUserAsync(user);
    }

    public async Task<bool> ValidateUser(string username, string password)
    {
        var user = await _userRepository.GetUserByUsername(username);

        if (user == null)
        {
            return false;
        }

        else
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }

    public async Task<List<UserDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        var usersDto = users.Select(u => u.ToUserDTO()).ToList();

        return usersDto;
    }
}

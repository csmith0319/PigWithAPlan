using PigWithAPlan.Server.Mappers;
using PigWithAPlan.Server.Models;

public interface IUserService
{
    Task<bool> RegisterAsync(User user);
    Task<bool> ValidateUser(User user);
    Task<List<UserViewModel>> GetAllAsync();
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> RegisterAsync(User user)
    {
        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ArgumentNullException("Password");
        }

        string password = user.Password;
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        var _newUser = new User
        {
            Name = user.Name,
            Email = user.Email,
            Username = user.Username,
            Password = hashedPassword
        };

        return await _userRepository.CreateUserAsync(_newUser);
    }

    public async Task<bool> ValidateUser(User user)
    {
        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
        {
            throw new ArgumentNullException("Username/Password");
        }

        var dbUser = await _userRepository.GetUserByUsername(user.Username);

        if (dbUser == null)
        {
            return false;
        }

        else
        {
            return BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password);
        }
    }

    public async Task<List<UserViewModel>> GetAllAsync()
    {
        var _users = await _userRepository.GetAllAsync();

        var users = _users.Select(u => u.ToUserViewModel()).ToList();

        return users;
    }
}

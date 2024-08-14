using Microsoft.EntityFrameworkCore;
using PigWithAPlan.Server.Data;
using PigWithAPlan.Server.Models;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<bool> CreateUserAsync(User user);
    Task<User?> GetUserByUsername(string username);
}

public class UserRepository : IUserRepository
{


    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        try
        {
            _context.Users.Add(user);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error has occured while creating a new user.");
            return false;
        }
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}

using Microsoft.EntityFrameworkCore;
using PigWithAPlan.Server.Data;
using PigWithAPlan.Server.Interfaces;
using PigWithAPlan.Server.Models;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
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
            return false;
        }
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}

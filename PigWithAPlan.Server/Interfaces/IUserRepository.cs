using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigWithAPlan.Server.Models;

namespace PigWithAPlan.Server.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<bool> CreateUserAsync(User user);
        Task<User?> GetUserByUsername(string username);
    }
}
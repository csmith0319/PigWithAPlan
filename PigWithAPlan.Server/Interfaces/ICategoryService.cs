using System.Collections.Generic;
using System.Threading.Tasks;
using PigWithAPlan.Server.Models;

namespace PigWithAPlan.Server.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
    }
}

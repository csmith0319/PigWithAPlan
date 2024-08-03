using System.Collections.Generic;
using System.Threading.Tasks;
using PigWithAPlan.Server.Models;

namespace PigWithAPlan.Server.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using PigWithAPlan.Server.Models;

namespace PigWithAPlan.Server.Repositories
{
    public interface ICategoryGroupRepository
    {
        Task<IEnumerable<CategoryGroup>> GetAllAsync();
        Task<CategoryGroup> GetByIdAsync(int id);
        Task<CategoryGroup> AddAsync(CategoryGroup categoryGroup);
        Task<CategoryGroup> UpdateAsync(CategoryGroup categoryGroup);
        Task<bool> DeleteAsync(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using PigWithAPlan.Server.Models;

namespace PigWithAPlan.Server.Services
{
    public interface ICategoryGroupService
    {
        Task<IEnumerable<CategoryGroup>> GetAllAsync();
        Task<CategoryGroup> GetByIdAsync(int id);
        Task<CategoryGroup> AddAsync(CategoryGroup categoryGroup);
        Task<CategoryGroup> UpdateAsync(CategoryGroup categoryGroup);
        Task<bool> DeleteAsync(int id);
    }
}

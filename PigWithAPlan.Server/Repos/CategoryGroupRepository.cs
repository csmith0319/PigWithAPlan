using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PigWithAPlan.Server.Data;
using PigWithAPlan.Server.Models;

namespace PigWithAPlan.Server.Repositories
{
    public interface ICategoryGroupRepository
    {
        Task<IEnumerable<CategoryGroup>> GetAllAsync();
        Task<CategoryGroup?> GetByIdAsync(int id);
        Task<CategoryGroup> AddAsync(CategoryGroup categoryGroup);
        Task<CategoryGroup> UpdateAsync(CategoryGroup categoryGroup);
        Task<bool> DeleteAsync(int id);
    }

    public class CategoryGroupRepository : ICategoryGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryGroup>> GetAllAsync()
        {
            return await _context.CategoryGroups.ToListAsync();
        }

        public async Task<CategoryGroup?> GetByIdAsync(int id)
        {
            return await _context.CategoryGroups.FindAsync(id);
        }

        public async Task<CategoryGroup> AddAsync(CategoryGroup categoryGroup)
        {
            _context.CategoryGroups.Add(categoryGroup);
            await _context.SaveChangesAsync();
            return categoryGroup;
        }

        public async Task<CategoryGroup> UpdateAsync(CategoryGroup categoryGroup)
        {
            _context.CategoryGroups.Update(categoryGroup);
            await _context.SaveChangesAsync();
            return categoryGroup;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoryGroup = await _context.CategoryGroups.FindAsync(id);
            if (categoryGroup == null)
            {
                return false;
            }

            _context.CategoryGroups.Remove(categoryGroup);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

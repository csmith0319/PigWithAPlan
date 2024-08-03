using System.Collections.Generic;
using System.Threading.Tasks;
using PigWithAPlan.Server.Models;
using PigWithAPlan.Server.Repositories;

namespace PigWithAPlan.Server.Services
{
    public class CategoryGroupService : ICategoryGroupService
    {
        private readonly ICategoryGroupRepository _repository;

        public CategoryGroupService(ICategoryGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryGroup>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CategoryGroup> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<CategoryGroup> AddAsync(CategoryGroup categoryGroup)
        {
            return await _repository.AddAsync(categoryGroup);
        }

        public async Task<CategoryGroup> UpdateAsync(CategoryGroup categoryGroup)
        {
            return await _repository.UpdateAsync(categoryGroup);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

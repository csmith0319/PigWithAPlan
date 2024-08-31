using System.Collections.Generic;
using System.Threading.Tasks;
using PigWithAPlan.Server.Models;
using PigWithAPlan.Server.Repositories;

namespace PigWithAPlan.Server.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync(int groupId);
        Task<CategoryViewModel?> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync(int groupId)
        {
            var _categories = await _repository.GetAllAsync(groupId);

            return _categories.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                CategoryGroupId = c.CategoryGroupId,
            }).ToList();
        }

        public async Task<CategoryViewModel?> GetByIdAsync(int id)
        {
            var _category = await _repository.GetByIdAsync(id);

            if (_category == null) return null;

            return new CategoryViewModel()
            {
                Id = _category.Id,
                Name = _category.Name,
                CategoryGroupId = _category.CategoryGroupId
            };
        }

        public async Task<Category> AddAsync(Category category)
        {
            return await _repository.AddAsync(category);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            return await _repository.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

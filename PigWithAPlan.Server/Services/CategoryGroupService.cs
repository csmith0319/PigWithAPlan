using System.Collections.Generic;
using System.Threading.Tasks;
using PigWithAPlan.Server.Mappers;
using PigWithAPlan.Server.Models;
using PigWithAPlan.Server.Repositories;

namespace PigWithAPlan.Server.Services
{
    public interface ICategoryGroupService
    {
        Task<IEnumerable<CategoryGroupViewModel>> GetAllAsync(int budgetId);
        Task<CategoryGroup?> GetByIdAsync(int id);
        Task<CategoryGroup> AddAsync(CategoryGroupCreateViewModel categoryGroup);
        Task<CategoryGroup> UpdateAsync(CategoryGroupCreateViewModel categoryGroup);
        Task<bool> DeleteAsync(int id);
    }

    public class CategoryGroupService : ICategoryGroupService
    {
        private readonly ICategoryGroupRepository _repository;

        public CategoryGroupService(ICategoryGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryGroupViewModel>> GetAllAsync(int budgetId)
        {
            var _categoryGroups = await _repository.GetAllAsync(budgetId);


            return _categoryGroups.Select(c => new CategoryGroupViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Categories = c.Category?.Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryGroupId = x.CategoryGroupId
                }).ToList()
            }).ToList();
        }

        public async Task<CategoryGroup?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<CategoryGroup> AddAsync(CategoryGroupCreateViewModel categoryGroupViewModel)
        {
            var categoryGroup = new CategoryGroup()
            {
                Name = categoryGroupViewModel.Name,
                BudgetId = categoryGroupViewModel.BudgetId,
            };

            return await _repository.AddAsync(categoryGroup);
        }

        public async Task<CategoryGroup> UpdateAsync(CategoryGroupCreateViewModel categoryGroupViewModel)
        {
            var categoryGroup = new CategoryGroup()
            {
                Name = categoryGroupViewModel.Name,
                BudgetId = categoryGroupViewModel.BudgetId,
            };

            return await _repository.UpdateAsync(categoryGroup);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

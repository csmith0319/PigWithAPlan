using PigWithAPlan.Server.Models;

namespace PigWithAPlan.Server.Mappers
{
    public static class CategoryGroupMappers
    {
        public static CategoryGroupCreateViewModel ToCreateViewModel(this CategoryGroup model)
        {
            return new CategoryGroupCreateViewModel
            {
                Name = model.Name,
                BudgetId = model.BudgetId
            };
        }

        public static CategoryGroup ToCreateModel(this CategoryGroupCreateViewModel viewModel)
        {
            if (viewModel == null) return null;

            return new CategoryGroup
            {
                Name = viewModel.Name,
                BudgetId = viewModel.BudgetId
            };
        }
    }
}
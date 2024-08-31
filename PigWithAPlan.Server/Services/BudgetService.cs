using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using PigWithAPlan.Server.Data;
using PigWithAPlan.Server.Models;
using System.Collections.Generic;
using System.Linq;


namespace PigWithAPlan.Server.Services
{
    public interface IBudgetService
    {
        IEnumerable<BudgetViewModel> GetAllBudgets();
        IEnumerable<BudgetTransactionViewModel> GetAllTransactions();
        Task<BudgetViewModel?> GetBudgetById(int id);
        Task<Budget> CreateBudget(BudgetCreateViewModel budget);
        Budget? UpdateBudget(int id, Budget budget);
        Task<bool> FavoriteBudget(int id);
        void DeleteBudget(int id);
    }

    public class BudgetService : IBudgetService
    {
        private readonly ApplicationDbContext _context;

        public BudgetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BudgetViewModel> GetAllBudgets()
        {
            var budgets = _context.Budgets
                .OrderByDescending(b => b.Favorite)
                .ThenByDescending(b => b.Favorite ? b.UpdatedAt : DateTime.MinValue)
                .ThenByDescending(b => !b.Favorite ? b.UpdatedAt : DateTime.MinValue)
                .Select(b => new BudgetViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Color = b.Color,
                    Favorite = b.Favorite,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt,
                    UserId = b.User.Id,
                    UserName = b.User.Name
                }).ToList();

            return budgets;
        }

        public IEnumerable<BudgetTransactionViewModel> GetAllTransactions()
        {
            var transactions = _context.Transactions
                                        .Include(t => t.Payee)
                                        .Select(t => new BudgetTransactionViewModel
                                        {
                                            TransactionId = t.Id,
                                            CreatedAt = t.CreatedAt,
                                            UpdatedAt = t.UpdatedAt,
                                            PayeeId = t.Payee != null ? t.Payee.Id : null,
                                            PayeeName = t.Payee != null ? t.Payee.Name : null,
                                        })
                                        .ToList();

            return transactions;
        }

        public async Task<BudgetViewModel?> GetBudgetById(int id)
        {
            var budget = await _context.Budgets.Select(b => new BudgetViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Color = b.Color,
                Favorite = b.Favorite,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                UserName = b.User.Name,
                UserId = b.User.Id
            }).FirstOrDefaultAsync(b => b.Id == id);

            return budget ?? null;
        }

        public async Task<Budget> CreateBudget(BudgetCreateViewModel viewModel)
        {
            var user = await _context.Users.FindAsync(1);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var budget = new Budget
            {
                Name = viewModel.Name,
                Color = viewModel.Color,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = 1,
                UpdatedBy = 1,
                UserId = user.Id,
                User = user
            };

            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<bool> FavoriteBudget(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);

            if (budget == null)
            {
                throw new Exception("Budget not found");
            }

            budget.Favorite = !budget.Favorite;
            budget.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public Budget? UpdateBudget(int id, Budget budget)
        {
            var existingBudget = _context.Budgets.Find(id);
            if (existingBudget == null)
            {
                return null;
            }
            existingBudget.Name = budget.Name;
            existingBudget.Color = budget.Color;
            existingBudget.UpdatedBy = budget.UpdatedBy;
            existingBudget.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return existingBudget;
        }

        public void DeleteBudget(int id)
        {
            var budget = _context.Budgets.Find(id);
            if (budget != null)
            {
                _context.Budgets.Remove(budget);
                _context.SaveChanges();
            }
        }
    }
}

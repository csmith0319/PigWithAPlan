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
        IEnumerable<BudgetViewDTO> GetAllBudgets();
        IEnumerable<BudgetTransactionViewDTO> GetAllTransactions();
        BudgetViewDTO? GetBudgetById(int id);
        Task<Budget> CreateBudget(BudgetCreateDTO budget);
        Budget? UpdateBudget(int id, Budget budget);
        void DeleteBudget(int id);
    }

    public class BudgetService : IBudgetService
    {
        private readonly ApplicationDbContext _context;

        public BudgetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BudgetViewDTO> GetAllBudgets()
        {
            var budgets = _context.Budgets
                .Select(b => new BudgetViewDTO 
                {
                    Id = b.Id,
                    Name = b.Name,
                    Color = b.Color,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt,
                    UserId = b.User.Id,
                    UserName = b.User.Name
                }).ToList();

            return budgets;
        }

        public IEnumerable<BudgetTransactionViewDTO> GetAllTransactions()
        {
            var transactions = _context.Transactions
                                        .Include(t => t.Payee)
                                        .Select(t => new BudgetTransactionViewDTO
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

        public BudgetViewDTO? GetBudgetById(int id)
        {
            var budget = _context.Budgets.Select(b => new BudgetViewDTO 
            {
                Id = b.Id,
                Name = b.Name,
                Color = b.Color,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                UserName = b.User.Name,
                UserId = b.User.Id
            }).FirstOrDefault(b => b.Id == id);

            return budget ?? null;
        }

        public async Task<Budget> CreateBudget(BudgetCreateDTO budgetDTO)
        {
            var user = await _context.Users.FindAsync(1);

            if (user == null) 
            {
                throw new Exception("User not found");
            }

            var budget = new Budget
            {
                Name = budgetDTO.Name,
                Color = budgetDTO.Color,
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

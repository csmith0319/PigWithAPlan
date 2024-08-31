using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PigWithAPlan.Server.Data;
using PigWithAPlan.Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace PigWithAPlan.Server.Services
{
    public interface ITransactionService
    {
        IEnumerable<TransactionDetailViewModel> GetAllTransactions();
        TransactionDetailViewModel? GetTransactionById(int id);
        Transaction CreateTransaction(Transaction transaction);
        Transaction? UpdateTransaction(int id, Transaction transaction);
        void DeleteTransaction(int id);
    }

    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TransactionDetailViewModel> GetAllTransactions()
        {
            var transactions = _context.Transactions
                                        .Include(t => t.Payee)
                                        .Include(t => t.Budget)
                                        .Select(t => new TransactionDetailViewModel
                                        {
                                            TransactionId = t.Id,
                                            CreatedAt = t.CreatedAt,
                                            UpdatedAt = t.UpdatedAt,
                                            PayeeName = t.Payee != null ? t.Payee.Name : null,
                                            BudgetName = t.Budget != null ? t.Budget.Name : null,
                                            BudgetColor = t.Budget != null ? t.Budget.Color : null,
                                            UserId = t.Budget != null ? t.Budget.User.Id : 0,
                                            UserName = t.Budget != null ? t.Budget.User.Name : null
                                        })
                                        .ToList();

            return transactions;
        }

        public TransactionDetailViewModel? GetTransactionById(int id)
        {
            var transaction = _context.Transactions
                                        .Include(t => t.Payee)
                                        .Include(t => t.Budget)
                                        .Select(t => new TransactionDetailViewModel
                                        {
                                            TransactionId = t.Id,
                                            CreatedAt = t.CreatedAt,
                                            UpdatedAt = t.UpdatedAt,
                                            PayeeName = t.Payee != null ? t.Payee.Name : null,
                                            BudgetName = t.Budget != null ? t.Budget.Name : null,
                                            BudgetColor = t.Budget != null ? t.Budget.Color : null,
                                            UserId = t.Budget != null ? t.Budget.User.Id : 0,
                                            UserName = t.Budget != null ? t.Budget.User.Name : null
                                        }).FirstOrDefault(t => t.TransactionId == id);

            return transaction ?? null;
        }

        public Transaction CreateTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public Transaction? UpdateTransaction(int id, Transaction transaction)
        {
            var existingTransaction = _context.Transactions.Find(id);
            if (existingTransaction == null)
            {
                return null;
            }

            existingTransaction.BudgetId = transaction.BudgetId;
            existingTransaction.PayeeId = transaction.PayeeId;
            existingTransaction.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
            return existingTransaction;
        }

        public void DeleteTransaction(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
            }
        }
    }
}

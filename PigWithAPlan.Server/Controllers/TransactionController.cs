using Microsoft.AspNetCore.Mvc;
using PigWithAPlan.Server.Models;
using PigWithAPlan.Server.Services;

namespace PigWithAPlan.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            var transactions = _transactionService.GetAllTransactions();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public IActionResult GetTransactionById(int id)
        {
            var transaction = _transactionService.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpPost]
        public IActionResult CreateTransaction([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdTransaction = _transactionService.CreateTransaction(transaction);
            return CreatedAtAction(nameof(GetTransactionById), new { id = createdTransaction.Id }, createdTransaction);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTransaction(int id, [FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedTransaction = _transactionService.UpdateTransaction(id, transaction);
            return Ok(updatedTransaction);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            _transactionService.DeleteTransaction(id);
            return NoContent();
        }
    }
}

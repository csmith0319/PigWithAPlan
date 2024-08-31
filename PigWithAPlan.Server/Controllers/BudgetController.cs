using Microsoft.AspNetCore.Mvc;
using PigWithAPlan.Server.Models;
using PigWithAPlan.Server.Services;

namespace PigWithAPlan.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet]
        public IActionResult GetAllBudgets()
        {
            var budgets = _budgetService.GetAllBudgets();
            return Ok(budgets);
        }

        [HttpGet("{id}/Transactions")]
        public IActionResult GetAllTransactions()
        {
            var budgets = _budgetService.GetAllTransactions();
            return Ok(budgets);
        }

        [HttpGet("{id}")]
        public IActionResult GetBudgetById(int id)
        {
            var budget = _budgetService.GetBudgetById(id);
            if (budget == null)
            {
                return NotFound();
            }
            return Ok(budget);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] BudgetCreateViewModel budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdBudget = await _budgetService.CreateBudget(budget);
            return CreatedAtAction(nameof(GetBudgetById), new { id = createdBudget.Id }, createdBudget);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBudget(int id, [FromBody] Budget budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedBudget = _budgetService.UpdateBudget(id, budget);
            return Ok(updatedBudget);
        }

        [HttpPost("Favorite")]
        public async Task<IActionResult> Favorite(int id)
        {
            try
            {
                if (id == 0) return NotFound();

                var success = await _budgetService.FavoriteBudget(id);

                return Ok(success);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBudget(int id)
        {
            _budgetService.DeleteBudget(id);
            return NoContent();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PigWithAPlan.Server.Models;
using PigWithAPlan.Server.Services;

namespace PigWithAPlan.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryGroupController : ControllerBase
    {
        private readonly ICategoryGroupService _service;
        private readonly IBudgetService _budgetService;

        public CategoryGroupController(ICategoryGroupService service, IBudgetService budgetService)
        {
            _service = service;
            _budgetService = budgetService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryGroupViewModel>>> GetAll(int budgetId)
        {
            var categoryGroups = await _service.GetAllAsync(budgetId);
            return Ok(categoryGroups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGroup>> GetById(int id)
        {
            var categoryGroup = await _service.GetByIdAsync(id);
            if (categoryGroup == null)
            {
                return NotFound();
            }

            return Ok(categoryGroup);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryGroup>> Create(CategoryGroupCreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int budgetId = viewModel.BudgetId;

                if (budgetId == 0)
                {
                    return BadRequest(new { success = false, message = "Budget ID does not exist." });
                }

                var createdCategoryGroup = await _service.AddAsync(viewModel);
                return CreatedAtAction(nameof(GetById), new { id = createdCategoryGroup.Id }, createdCategoryGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Failed to create category group." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryGroupCreateViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(viewModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

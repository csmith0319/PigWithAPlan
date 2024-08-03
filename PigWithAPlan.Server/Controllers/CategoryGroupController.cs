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

        public CategoryGroupController(ICategoryGroupService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryGroup>>> GetAll()
        {
            var categoryGroups = await _service.GetAllAsync();
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
        public async Task<ActionResult<CategoryGroup>> Create(CategoryGroup categoryGroup)
        {
            var createdCategoryGroup = await _service.AddAsync(categoryGroup);
            return CreatedAtAction(nameof(GetById), new { id = createdCategoryGroup.Id }, createdCategoryGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryGroup categoryGroup)
        {
            if (id != categoryGroup.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(categoryGroup);
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

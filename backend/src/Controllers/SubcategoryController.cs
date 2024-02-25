using ContactListAPI.Dtos;
using ContactListAPI.Models;
using ContactListAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactListAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class SubcategoryController(SubcategoryService subcategoryService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subcategory>>> Get()
    {
        var subcategories = await subcategoryService.GetAsync();

        return Ok(subcategories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Subcategory>> GetById(int id)
    {
        var subcategory = await subcategoryService.GetByIdAsync(id);

        return Ok(subcategory);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddSubcategoryRequest request)
    {
        var subcategoryId = await subcategoryService.AddAsync(request);

        var controllerName = ControllerContext.ActionDescriptor.ControllerName;
        var url = $"/{controllerName}/{subcategoryId}";

        return Created(url, null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await subcategoryService.DeleteAsync(id);

        return Ok();
    }
}

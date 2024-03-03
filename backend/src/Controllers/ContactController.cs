using ContactListAPI.Dtos;
using ContactListAPI.Models;
using ContactListAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactListAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ContactController(ContactService contactService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactDto>>> Get()
    {
        var contacts = await contactService.GetAsync();

        return Ok(contacts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContactDto>> GetById(int id)
    {
        var contact = await contactService.GetByIdAsync(id);

        return Ok(contact);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddContactRequest request)
    {
        var contactId = await contactService.AddAsync(request);

        var controllerName = ControllerContext.ActionDescriptor.ControllerName;
        var url = $"/{controllerName}/{contactId}";
        return Created(url, null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await contactService.DeleteAsync(id);

        return Ok();
    }
}

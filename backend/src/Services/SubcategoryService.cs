using ContactListAPI.Data;
using ContactListAPI.Dtos;
using ContactListAPI.Exceptions;
using ContactListAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ContactListAPI.Services;

public class SubcategoryService(ContactListDbContext dbContext)
{
    public async Task<IEnumerable<Subcategory>> GetAsync()
    {
        var subcategories = await dbContext.Subcategories.ToListAsync();

        return subcategories;
    }

    public async Task<Subcategory> GetByIdAsync(int id)
    {
        var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new ApiException($"Subcategory with id {id} could not be found.", HttpStatusCode.NotFound);

        return subcategory;
    }

    public async Task<int> AddAsync(AddSubcategoryRequest request)
    {
        // Capitalize the first letter and make the rest of the letters lowercase
        var name = char.ToUpper(request.Name[0]) + request.Name[1..].ToLower();

        if (await dbContext.Subcategories.AnyAsync(x => x.Name == name))
            throw new ApiException($"Subcategory with name {name} already exists.", HttpStatusCode.BadRequest);

        var subcategory = new Subcategory { Name = name, CategoryId = 3 };

        await dbContext.Subcategories.AddAsync(subcategory);
        await dbContext.SaveChangesAsync();

        return subcategory.Id;
    }

    public async Task DeleteAsync(int id)
    {
        if (id ==1 || id == 2 || id == 3)
        {
            throw new ApiException("Cannot delete base subcategories.", HttpStatusCode.BadRequest);
        }

        var subcategory = await dbContext.Subcategories
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new ApiException($"Subcategory with id {id} could not be found.", HttpStatusCode.NotFound);

        dbContext.Subcategories.Remove(subcategory);
        await dbContext.SaveChangesAsync();
    }
}

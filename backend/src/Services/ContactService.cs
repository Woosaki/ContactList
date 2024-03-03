using ContactListAPI.Data;
using ContactListAPI.Dtos;
using ContactListAPI.Exceptions;
using ContactListAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ContactListAPI.Services;

public class ContactService(ContactListDbContext dbContext)
{
    public async Task<IEnumerable<Contact>> GetAsync()
    {
        var contacts = await dbContext.Contacts.ToListAsync();

        return contacts;
    }

    public async Task<Contact> GetByIdAsync(int id)
    {
        var contact = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new ApiException($"Contact with id {id} could not be found", HttpStatusCode.NotFound);

        return contact;
    }

    public async Task<int> AddAsync(AddContactRequest request)
    {
        // Category is private
        if (request.CategoryId == 2 && request.SubcategoryId.HasValue)
            throw new ApiException($"SubcategoryId should not be provided when CategoryId is 2", HttpStatusCode.BadRequest);

        // Category is business
        if (request.CategoryId == 1 && (!request.SubcategoryId.HasValue || request.SubcategoryId < 1 || request.SubcategoryId > 3))
            throw new ApiException($"SubcategoryId allowed values for business contact is 1, 2 or 3", HttpStatusCode.BadRequest);

        // Category is other
        if (request.CategoryId == 3 && (!request.SubcategoryId.HasValue || request.SubcategoryId <= 3))
            throw new ApiException($"SubcategoryId for other contacts should be provided and be greater than 3", HttpStatusCode.BadRequest);

        if (request.SubcategoryId.HasValue)
        {
            var subcategoryExists = await dbContext.Subcategories.AnyAsync(x => x.Id == request.SubcategoryId);
            if (!subcategoryExists)
            {
                throw new ApiException($"Subcategory with id {request.SubcategoryId} could not be found", HttpStatusCode.NotFound);
            }
        }

        // Capitalize the first letter and make the rest of the letters lowercase
        var firstName = char.ToUpper(request.FirstName[0]) + request.FirstName[1..].ToLower();
        var lastName = char.ToUpper(request.LastName[0]) + request.LastName[1..].ToLower();

        var contact = new Contact
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = request.PhoneNumber,
            Birthdate = request.Birthdate,
            CategoryId = request.CategoryId,
            SubcategoryId = request.SubcategoryId
        };

        await dbContext.Contacts.AddAsync(contact);
        await dbContext.SaveChangesAsync();

        return contact.Id;
    }


    public async Task DeleteAsync(int id)
    {
        var contact = await dbContext.Contacts
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new ApiException($"Contact with id {id} could not be found", HttpStatusCode.NotFound);

        dbContext.Contacts.Remove(contact);
        await dbContext.SaveChangesAsync();
    }
}

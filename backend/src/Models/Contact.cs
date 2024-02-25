namespace ContactListAPI.Models;

public class Contact
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime Birthdate { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;
}

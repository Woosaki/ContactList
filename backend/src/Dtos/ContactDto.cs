namespace ContactListAPI.Dtos;

public record ContactDto
(
    int Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    int Birthyear,
    int Birthday,
    int Birthmonth,
    string Category,
    string? Subcategory
);

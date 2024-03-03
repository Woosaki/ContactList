using System.ComponentModel.DataAnnotations;

namespace ContactListAPI.Dtos;

public record AddContactRequest
(
    string FirstName,

    string LastName,

    [RegularExpression(@"^[0-9]{3} [0-9]{3} [0-9]{3}$", ErrorMessage = "Invalid phone number format.")]
    string PhoneNumber,

    [DataType(DataType.Date)]
    DateTime Birthdate,

    [AllowedValues([1, 2, 3])]
    int CategoryId,

    int? SubcategoryId
);

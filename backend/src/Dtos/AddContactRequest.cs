namespace ContactListAPI.Dtos;

public record AddContactRequest
(
    string FirstName,

    string LastName,

    string PhoneNumber,

    DateTime Birthdate,

    int CategoryId,

    int? SubcategoryId
);

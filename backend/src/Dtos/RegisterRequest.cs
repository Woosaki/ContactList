using System.ComponentModel.DataAnnotations;

namespace ContactListAPI.Dtos;

public record RegisterRequest
(
    [EmailAddress] string Email,
    string Password,
    string ConfirmPassword
);

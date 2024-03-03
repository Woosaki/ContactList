using System.ComponentModel.DataAnnotations;

namespace ContactListAPI.Dtos;

public record LoginRequest
(
    [EmailAddress] string Email,
    string Password
);

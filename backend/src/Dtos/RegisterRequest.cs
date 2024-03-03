namespace ContactListAPI.Dtos;

public record RegisterRequest
(
    string Email,
    string Password,
    string ConfirmPassword
);

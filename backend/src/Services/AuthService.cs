using ContactListAPI.Data;
using ContactListAPI.Dtos;
using System.Security.Claims;

namespace ContactListAPI.Services;

public class AuthService(ContactListDbContext dbContext)
{
    public ClaimsPrincipal Login(LoginRequest request)
    {
        throw new NotImplementedException();
    }

    public bool Register(RegisterRequest request)
    {
        throw new NotImplementedException();
    }
}

﻿using BCrypt.Net;
using ContactListAPI.Data;
using ContactListAPI.Dtos;
using ContactListAPI.Exceptions;
using ContactListAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace ContactListAPI.Services;

public class AuthService(ContactListDbContext dbContext)
{
    public async Task<ClaimsPrincipal> Login(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ApiException("Username and password must be specified.", HttpStatusCode.BadRequest);
        }

        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email)
            ?? throw new ApiException($"There is no user with email {request.Email}.", HttpStatusCode.NotFound);

        if (BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, "Identity");

            return new ClaimsPrincipal(identity);
        }

        throw new ApiException("Invalid password.", HttpStatusCode.Unauthorized);
    }

    public async Task Register(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.ConfirmPassword))
        {
            throw new ApiException("Email, password and confirm password must be specified.", HttpStatusCode.BadRequest);
        }

        if (request.Password != request.ConfirmPassword)
        {
            throw new ApiException("Password and confirm password do not match.", HttpStatusCode.BadRequest);
        }

        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user != null)
        {
            throw new ApiException($"User with email {request.Email} already exists.", HttpStatusCode.BadRequest);
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        await dbContext.Users.AddAsync(new User
        {
            Email = request.Email,
            Password = hashedPassword
        });

        await dbContext.SaveChangesAsync();
    }
}

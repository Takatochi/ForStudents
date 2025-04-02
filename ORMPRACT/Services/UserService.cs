using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ORMPRACT.Data;
using ORMPRACT.Models;

namespace ORMPRACT.Services;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task AddUserAsync(string username, string password)
    {
        var newUser = new User
        {
            Username = username,
            PasswordHash = password,
        }; 
        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, password);
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
    }
    
}
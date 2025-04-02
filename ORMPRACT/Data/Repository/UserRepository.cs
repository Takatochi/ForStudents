using Microsoft.EntityFrameworkCore;
using ORMPRACT.Models;

namespace ORMPRACT.Data.Repository;

public class UserRepository (AppDbContext dbContext) : IUserRepository
{
    public async Task<User> CreateAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await dbContext.Users.FindAsync(id);
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task<List<User>> GetFollowersAsync(int userId)
    {
        return await dbContext.Follows
            .Where(f => f.FollowingId == userId)
            .Select(f => f.Follower).ToListAsync();
    }

    public async Task<List<User>> GetFollowingAsync(int userId)
    {
        return await dbContext.Follows
            .Where(f => f.FollowerId == userId)
            .Select(f => f.Following)
            .ToListAsync();
    }

    public async Task<User> UpdateAsync(User user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task RemoveAsync(User user)
    {
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
    }
}
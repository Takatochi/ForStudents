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
    
    // Вразливий SQL-запит: введене користувачем значення напряму вставляється в рядок SQL-запиту,
    // що відкриває можливість для SQL-ін'єкції. Pass ' OR '1'='1
    public User Login(string password)
    {
        //Користувач може передати шкідливий SQL-код, в password = Pass ' OR '1'='1
        string query = "SELECT * FROM users WHERE password_hash = '" + password + "'";
        Console.WriteLine("Executing SQL: " + query); // Логування SQL-запиту (ще один небезпечний момент)
        return dbContext.Users.FromSqlRaw(query).FirstOrDefault();

    }
    //Цей варіант використовує FromSqlInterpolated, який автоматично підставляє параметри безпечним способом
    public User LoginSQL(string password)
    {
        return dbContext.Users
            .FromSqlInterpolated($"SELECT * FROM users WHERE password_hash = {password}")
            .FirstOrDefault();
    }
    //Рекомендується уникати FromSqlRaw, якщо можна зробити запит через LINQ
    public User LoginORM(string password)
    {
        return dbContext.Users
            .Where(u => u.PasswordHash == password)
            .FirstOrDefault();
    }
    
}
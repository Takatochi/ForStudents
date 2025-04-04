﻿using ORMPRACT.Models;

namespace ORMPRACT.Data.Repository;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<User> GetByIdAsync(int id);
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByEmailAsync(string email);
    Task<List<User>> GetAllAsync();
    Task<List<User>> GetFollowersAsync(int userId);
    Task<List<User>> GetFollowingAsync(int userId);
    Task<User> UpdateAsync(User user);
    Task RemoveAsync(User user);
    
    [Obsolete("Цей метод вразливий до SQL-ін'єкцій! Використовуйте безпечну альтернативу.")]
    User Login(string password);
    User LoginSQL(string password);
    User LoginORM(string password);
}
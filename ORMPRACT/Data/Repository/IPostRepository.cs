using ORMPRACT.Models;

namespace ORMPRACT.Data.Repository;

public interface IPostRepository
{
    Task<Post> CreateAsync(Post post);
    Task<Post> GetByIdAsync(int id);
    Task<List<Post>> GetAllAsync();
    Task<List<Post>> GetByUserIdAsync(int userId);
    Task<List<Post>> GetFeedAsync(int userId, int limit, int offset); // Стрічка новин
    Task<Post> UpdateAsync(Post post);
    Task RemoveAsync(Post post);
}
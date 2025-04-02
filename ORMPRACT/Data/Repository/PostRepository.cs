using Microsoft.EntityFrameworkCore;
using ORMPRACT.Models;

namespace ORMPRACT.Data.Repository;

public class PostRepository(AppDbContext dbContext) : IPostRepository
{
    public async Task<Post> CreateAsync(Post post)
    {
        await dbContext.Posts.AddAsync(post);
        await dbContext.SaveChangesAsync();
        return post;
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        return await dbContext.Posts.FindAsync(id);
    }

    public async Task<List<Post>> GetAllAsync()
    {
        return await dbContext.Posts.ToListAsync();
    }

    public async Task<List<Post>> GetByUserIdAsync(int userId)
    {
        return await dbContext.Posts
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Post>> GetFeedAsync(int userId, int limit, int offset)
    {
        var followingIds = await dbContext.Follows
            .Where(f => f.FollowerId == userId)
            .Select(f => f.FollowingId)
            .ToListAsync();

        return await dbContext.Posts
            .Where(p => followingIds.Contains(p.UserId))
            .OrderByDescending(p => p.CreatedAt)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        dbContext.Posts.Update(post);
        await dbContext.SaveChangesAsync();
        return post;
    }

    public async Task RemoveAsync(Post post)
    {
        dbContext.Posts.Remove(post);
        await dbContext.SaveChangesAsync();
    }
}
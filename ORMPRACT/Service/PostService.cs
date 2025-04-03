using Microsoft.AspNetCore.Mvc;
using ORMPRACT.Data.Repository;
using ORMPRACT.Models;
using TravelGateway.DTO;

namespace ORMPRACT.Service;

public class PostService
{
    private readonly IPostRepository _context;
    private readonly IUserRepository _contextUser;

    public PostService(IPostRepository postRepository, IUserRepository userRepository)
    {
        _context = postRepository;
        _contextUser = userRepository;
    }

    public async Task<List<Post>> GetAllPostsAsync(int userId, FeedRequest feedRequest)
    {
        return await _context.GetFeedAsync(userId, feedRequest.limit, feedRequest.offset);
    }
    
    public User Login(string password)
    {
        return _contextUser.Login(password);
    }
    
    
}
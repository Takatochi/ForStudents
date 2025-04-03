using Microsoft.AspNetCore.Mvc;
using ORMPRACT.Data.Repository;
using ORMPRACT.Service;
using TravelGateway.DTO;

namespace ORMPRACT.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    // private readonly IPostRepository _postRepository;
    private readonly PostService _service;
    public PostsController( PostService service)
    {
      _service = service;
    }

    [HttpGet("hi")]
    public IActionResult hillle()
    {
        return Ok("Hello!");
    }
    
    [HttpGet("feed/{userId}")]
    public async Task<IActionResult> GetFeed(int userId, [FromQuery] FeedRequest request)
    {
        var posts =  await _service.GetAllPostsAsync(userId, request);
        return Ok(posts);
    }
    [HttpGet("feed/ps")]
    public IActionResult login(string password)
    {
        var user = _service.Login(password);
        return Ok(user);
    }
}
using Microsoft.AspNetCore.Mvc;
using ORMPRACT.Data.Repository;

namespace ORMPRACT.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _postRepository;

    public PostsController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    [HttpGet("hi")]
    public IActionResult hillle()
    {
        return Ok("Hello!");
    }
    [HttpGet("feed/{userId}")]
    public async Task<IActionResult> GetFeed(int userId, [FromQuery] int limit = 10, [FromQuery] int offset = 0)
    {
        var posts = await _postRepository.GetFeedAsync(userId, limit, offset);
        return Ok(posts);
    }
}
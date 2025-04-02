namespace ORMPRACT.Models;

public class Post : BaseModel
{
    public int UserId { get; set; }
    public string Content { get; set; }
    public User User { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
}
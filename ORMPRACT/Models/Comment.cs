namespace ORMPRACT.Models;

public class Comment : BaseModel
{
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
    public Post Post { get; set; }
    public User User { get; set; }
    public ICollection<Like> Likes { get; set; } = new List<Like>();
}
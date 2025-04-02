namespace ORMPRACT.Models;

public class Like : BaseModel
{
    public int? PostId { get; set; }
    public int? CommentId { get; set; }
    public int UserId { get; set; }
    public Post Post { get; set; }
    public Comment Comment { get; set; }
    public User User { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMPRACT.Models;

public class User : BaseModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    [Column("password_hash")]
    public string PasswordHash { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    public ICollection<Follow> Followers { get; set; } = new List<Follow>();
    public ICollection<Follow> Following { get; set; } = new List<Follow>();
    
}
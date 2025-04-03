using Microsoft.EntityFrameworkCore;
using ORMPRACT.Models;

namespace ORMPRACT.Data{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments{ get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Follow> Follows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(
                entity =>
                { 
                    // прикклад User → Users не потрібно вказувати
                    entity.ToTable("users");
                    // прикклад Chuvak → Users  потрібно вказувати
                    entity.HasKey(e => e.Id);
                    entity.HasIndex(u => u.Username).IsUnique();
                    entity.HasIndex(u => u.Email).IsUnique();
                    entity.Property(e => e.PasswordHash)
                        .HasColumnName("password_hash");
                    // entity.Property(u => u.CreatedAt)
                    //     .HasConversion(
                    //         v => v.ToUniversalTime(), // Перетворюємо в UTC перед збереженням
                    //         v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Перетворюємо назад в UTC після зчитування
                });
            modelBuilder.Entity<Post>(
                entity =>
                {
                    // не потрібно вказувати entity.ToTable("posts");
                    entity.HasKey(e => e.Id);
                    entity.HasOne(p => p.User)
                        .WithMany(u => u.Posts)
                        .HasForeignKey(p => p.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
                   
                });
            modelBuilder.Entity<Comment>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.HasOne(c => c.Post)
                        .WithMany(p => p.Comments)
                        .HasForeignKey(c => c.PostId)
                        .OnDelete(DeleteBehavior.Cascade);
                    
                    entity.HasOne(c => c.User)
                        .WithMany(u => u.Comments)
                        .HasForeignKey(c => c.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
                    
                 
                });
            modelBuilder.Entity<Like>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.HasIndex(l => new { l.UserId, l.PostId, l.CommentId })
                        .IsUnique();
                    
                    entity.HasOne(l => l.Post)
                        .WithMany(p => p.Likes)
                        .HasForeignKey(l => l.PostId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired(false);
                    
                    entity.HasOne(l => l.Comment)
                        .WithMany(c => c.Likes)
                        .HasForeignKey(l => l.CommentId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired(false);
                    
                    entity.HasOne(l => l.User)
                        .WithMany(u => u.Likes)
                        .HasForeignKey(l => l.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
                });
            
            modelBuilder.Entity<Follow>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.HasIndex(e => new { e.FollowerId, e.FollowingId })
                        .IsUnique(); // Уникальная пара follower+following
                    
                    entity.HasOne(f => f.Follower)
                        .WithMany(u => u.Following)
                        .HasForeignKey(f => f.FollowerId)
                        .OnDelete(DeleteBehavior.Cascade);
                    
                    entity. HasOne(f => f.Following)
                        .WithMany(u => u.Followers)
                        .HasForeignKey(f => f.FollowingId)
                        .OnDelete(DeleteBehavior.Cascade);
                    
                });
            
        }
    }
}

using ORMPRACT.Data;
using ORMPRACT.Models;
using ORMPRACT.Models;

namespace ORMPRACT.Data.Seeder;

public static class DatabaseSeeder
{
    public static void SeedTestData(AppDbContext context)
    {
        // Сідінг користувачів
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { Username = "ivan_petrenko", Email = "ivan.petrenko@email.com", PasswordHash = "hash1", CreatedAt = DateTime.Now },
                new User { Username = "maria_ivanenko", Email = "maria.ivanenko@email.com", PasswordHash = "hash2", CreatedAt = DateTime.Now },
                new User { Username = "oleg_shevchenko", Email = "oleg.shevchenko@email.com", PasswordHash = "hash3", CreatedAt = DateTime.Now }
            );
            context.SaveChanges();
        }

        // Сідінг постів
        if (!context.Posts.Any())
        {
            context.Posts.AddRange(
                new Post { UserId = 1, Content = "Мій перший пост у цій соцмережі!", CreatedAt = DateTime.Now },
                new Post { UserId = 2, Content = "Привіт усім! Як справи?", CreatedAt = DateTime.Now },
                new Post { UserId = 3, Content = "Щойно прочитав цікаву книгу, рекомендую.", CreatedAt = DateTime.Now }
            );
            context.SaveChanges();
        }

        // Сідінг коментарів
        if (!context.Comments.Any())
        {
            context.Comments.AddRange(
                new Comment { PostId = 1, UserId = 2, Content = "Вітаю! Раді бачити тебе тут.", CreatedAt = DateTime.Now },
                new Comment { PostId = 1, UserId = 3, Content = "Гарний пост, продовжуй писати!", CreatedAt = DateTime.Now },
                new Comment { PostId = 2, UserId = 1, Content = "Все чудово, дякую! А у тебе як?", CreatedAt = DateTime.Now }
            );
            context.SaveChanges();
        }

        // Сідінг лайків
        if (!context.Likes.Any())
        {
            context.Likes.AddRange(
                new Like { UserId = 2, PostId = 1, CreatedAt = DateTime.Now }, // Марія лайкнула пост Івана
                new Like { UserId = 3, PostId = 1, CreatedAt = DateTime.Now }, // Олег лайкнув пост Івана
                new Like { UserId = 1, CommentId = 1, CreatedAt = DateTime.Now }, // Іван лайкнув коментар Марії
                new Like { UserId = 3, CommentId = 2, CreatedAt = DateTime.Now }  // Олег лайкнув коментар свій
            );
            context.SaveChanges();
        }

        // Сідінг підписок
        if (!context.Follows.Any())
        {
            context.Follows.AddRange(
                new Follow { FollowerId = 1, FollowingId = 2, CreatedAt = DateTime.Now }, // Іван підписався на Марію
                new Follow { FollowerId = 1, FollowingId = 3, CreatedAt = DateTime.Now }, // Іван підписався на Олега
                new Follow { FollowerId = 2, FollowingId = 1, CreatedAt = DateTime.Now }, // Марія підписалася на Івана
                new Follow { FollowerId = 3, FollowingId = 2, CreatedAt = DateTime.Now }  // Олег підписався на Марію
            );
            context.SaveChanges();
        }
    }
}
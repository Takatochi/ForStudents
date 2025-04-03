using Microsoft.EntityFrameworkCore;
using ORMPRACT.Data;
using ORMPRACT.Data.Repository;
using ORMPRACT.Data.Seeder;
using ORMPRACT.Service;

namespace ORMPRACT;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();
        
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<PostService>();
        
        builder.Services.AddSwaggerGen(); // додаємо Swagger генератор

        // Аналогічно для CommentRepository, LikeRepository, FollowRepository
        
        // builder.Services.AddScoped<TokenService>();
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"))
            );
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); // додає OpenAPI документацію
            app.UseSwaggerUI(); // для доступу через браузер
        }
        

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // context.Database.Migrate();
            
            DatabaseSeeder.SeedTestData(context);
            
        }
        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapControllers(); 
        

        app.Run();
    }
}
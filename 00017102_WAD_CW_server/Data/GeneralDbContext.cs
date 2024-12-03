using _00017102_WAD_CW_server.models;
using Microsoft.EntityFrameworkCore;

namespace _00017102_WAD_CW_server.Data
{
    public class GeneralDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public GeneralDbContext(DbContextOptions<GeneralDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 1,
                Name = "Unknown"
            });
        }
    }
}

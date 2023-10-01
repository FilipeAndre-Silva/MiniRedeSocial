using AnySocialNetwork.Data.Mappers;
using AnySocialNetwork.Models;
using Microsoft.EntityFrameworkCore;

namespace AnySocialNetwork.Data
{
    public class AnySocialNetworkDbContext : DbContext
    {
        public AnySocialNetworkDbContext(DbContextOptions<AnySocialNetworkDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PostMapper());
            modelBuilder.ApplyConfiguration(new UserMapper());
        }
    }
}
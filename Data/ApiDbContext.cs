using Microsoft.EntityFrameworkCore;
using BlogCrudApp.Models;

namespace BlogCrudApp.Data
{
    public class ApiDbContext : DbContext
    {
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>(entity => {
                entity.HasOne(p => p.Post)
                    .WithMany(c => c.Comments)
                    .HasForeignKey(x => x.PostId);
            });
        }
    }
}
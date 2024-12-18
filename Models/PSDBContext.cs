using Microsoft.EntityFrameworkCore;

namespace Models;

public class PSDBContext : DbContext
{
    public virtual DbSet<Post> Posts { get; set; }

    // Empty constructor for testing
    public PSDBContext() { }

    public PSDBContext(DbContextOptions<PSDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Posts Table
        modelBuilder.Entity<Post>(entity =>
        {
            //Primary Key
            entity.HasKey(p => p.postID);
            entity.Property(p => p.postID).ValueGeneratedOnAdd();

            //Columns
            entity.Property(p => p.timestamp);
            entity.Property(p => p.repliedToPostID);
            entity.Property(p => p.username);
            entity.Property(p => p.content);
        });
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("PSDBConnectionString");
        optionsBuilder.UseSqlServer(connectionString);
    }
}
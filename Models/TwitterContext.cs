using Microsoft.EntityFrameworkCore;

namespace TwitterClone.Models
{
    public class TwitterContext : DbContext
    {
        public TwitterContext(DbContextOptions<TwitterContext> options)
            : base (options)
        { }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}
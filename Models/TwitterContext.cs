using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TwitterClone.Models;

namespace TwitterClone.Models
{
    public class TwitterContext : DbContext
    {
        public TwitterContext(DbContextOptions<TwitterContext> options)
            : base (options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
    }
}
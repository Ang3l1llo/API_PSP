using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options)
            : base(options)
        { }

        public DbSet<Player> Players { get; set; }
    }
}
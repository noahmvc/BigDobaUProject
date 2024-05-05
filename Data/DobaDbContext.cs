using BigDobaUProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BigDobaUProject.Data
{
    public class DobaDbContext : DbContext
    {
        public DobaDbContext(DbContextOptions<DobaDbContext> options) : base(options) { }

        public DbSet<Music> Musics { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            // You can also configure your entities here if necessary
        }
    }
}

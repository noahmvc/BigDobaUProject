using Microsoft.EntityFrameworkCore;

namespace BigDobaUProject.Data
{
    public class DobaDbContext : DbContext
    {
        public DobaDbContext() { }
        public DobaDbContext(DbContextOptions<DobaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
        }
    }
}

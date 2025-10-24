using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using YetAnotherCartApi.Auth.Entity;

namespace YetAnotherCartApi.Auth.Context
{
    public class UserContext : DbContext
    {
        public DbSet<ShopUser> Users { get; set; }
        public DbSet<UserCapability> Capabilities { get; set; }

        public UserContext(DbContextOptions<UserContext> contextOptions) : base(contextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<ShopUser>().ToTable("users", schema: "shop"); 
        }
    }
}

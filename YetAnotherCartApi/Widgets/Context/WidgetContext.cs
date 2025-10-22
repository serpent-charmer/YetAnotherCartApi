using Microsoft.EntityFrameworkCore;
using YetAnotherCartApi.Auth.Entity;
using YetAnotherCartApi.Cart.Context;
using YetAnotherCartApi.Widgets.Entity;

namespace YetAnotherCartApi.Widgets.Context
{
    public class WidgetContext : DbContext
    {
        public DbSet<UserShop> UserShop { get; set; }    
        public DbSet<ShopWidget> Widgets { get; set; }    
        public WidgetContext(DbContextOptions<WidgetContext> contextOptions) : base(contextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserShop>().ToTable("shop_contents", schema: "shop");
            modelBuilder.Entity<ShopWidget>().ToTable("widgets", schema: "shop");
        }
    }
}

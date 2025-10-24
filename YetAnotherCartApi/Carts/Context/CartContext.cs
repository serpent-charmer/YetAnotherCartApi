using Microsoft.EntityFrameworkCore;
using YetAnotherCartApi.Carts.Entity;
using YetAnotherCartApi.Widgets.Entity;

namespace YetAnotherCartApi.Carts.Context
{
    public class CartContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartListing> CartListings { get; set; }
        public CartContext(DbContextOptions<CartContext> contextOptions) : base(contextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().ToTable("cart_content", schema: "shop");
            modelBuilder.Entity<CartListing>().ToTable("v_cart_content", schema: "shop");
        }
    }
}

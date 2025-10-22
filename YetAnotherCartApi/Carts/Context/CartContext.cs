using Microsoft.EntityFrameworkCore;

namespace YetAnotherCartApi.Cart.Context
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> contextOptions) : base(contextOptions)
        {
        }
    }
}

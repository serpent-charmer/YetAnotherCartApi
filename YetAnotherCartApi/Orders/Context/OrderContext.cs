using Microsoft.EntityFrameworkCore;
using YetAnotherCartApi.Orders.Entity;

namespace YetAnotherCartApi.Orders.Context
{
    public class OrderContext : DbContext
    {

        public DbSet<Order> Orders {  get; set; }   
        public DbSet<Warehouse> Warehouses {  get; set; }   
        public DbSet<Invoice> Invoices {  get; set; }   
        public DbSet<InvoiceInfo> InvoiceInfo {  get; set; }   
        public OrderContext(DbContextOptions<OrderContext> contextOptions) : base(contextOptions)
        {
        }
    }
}

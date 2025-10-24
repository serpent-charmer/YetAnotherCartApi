using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherCartApi.Orders.Entity
{
    [Table("invoice_info", Schema = "shop")]
    public class InvoiceInfo
    {
        [Column("order_id")]
        public Guid OrderId { get; set; }
        [Column("price")]
        public int Price { get; set; }
        [Column("buyer")]
        public Guid Buyer { get; set; }
        [Column("seller")]
        public Guid Seller { get; set; }
    }
}

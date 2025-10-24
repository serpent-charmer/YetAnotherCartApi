using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherCartApi.Orders.Entity
{
    [Table("invoice", Schema = "shop")]
    public class Invoice
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("order_id")]
        public Guid OrderId { get; set; }
        [Column("price")]
        public int Price { get; set; }
        [Column("buyer")]
        public Guid Buyer {  get; set; }
        [Column("seller")]
        public Guid Seller { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherCartApi.Orders.Entity
{
    [Table("order", Schema = "shop")]
    public class Order
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("status")]
        public string Status { get; set; }
        [Column("widget_id")]
        public Guid WidgetId { get; set; }
        [Column("uid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid Uid { get; set; }
        [Column("arrive_at")]
        public Guid ArriveAt { get; set; }
        [Column("buyer")]
        public Guid Buyer { get; set; }
    }
}

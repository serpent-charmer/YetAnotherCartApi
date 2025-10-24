using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherCartApi.Carts.Entity
{
    public class Cart
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("widget_id")]
        public Guid WidgetId { get; set; }
    }
}

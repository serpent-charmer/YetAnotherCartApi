using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherCartApi.Carts.Entity
{
    public class CartListing
    {
        [Column("widget_id")]
        public Guid widgetId { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("price")]
        public int price { get; set; }
        [Column("widget_name")]
        public string widgetName { get; set; }
    }
}

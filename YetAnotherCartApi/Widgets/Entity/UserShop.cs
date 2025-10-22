using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherCartApi.Widgets.Entity
{
    public class UserShop
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("widget_id")]
        public Guid WidgetId { get; set; }
        [Column("is_public")]
        public bool IsPublic { get; set; }
    }
}

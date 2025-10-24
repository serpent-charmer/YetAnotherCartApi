using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherCartApi.Widgets.Entity
{
    public class ShopWidget
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("widget_name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }
        [Column("price")]
        public int Price { get; set; }
        [Column("uid")]
        public Guid Uid { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
    }
}

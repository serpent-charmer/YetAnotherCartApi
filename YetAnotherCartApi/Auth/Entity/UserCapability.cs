using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherCartApi.Auth.Entity
{
    [Table("capability", Schema = "shop")]
    public class UserCapability
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]]
        public int Id { get; set; }
        [Column("user_id")]
        public Guid Uid {  get; set; }
        [Column("capability")]
        public string Capability { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherCartApi.Auth.Entity
{
    public class ShopUser
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Column("uid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid Uid { get; set; }
        [Column("username")]
        public string Username { get; set; }
        
    }
}

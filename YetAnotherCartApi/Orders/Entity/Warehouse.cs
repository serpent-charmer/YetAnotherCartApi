using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YetAnotherCartApi.Orders.Entity
{
    public class Warehouse
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("uid")]
        public string Uid { get; set; }
        [Column("address")]
        public string Address { get; set; }
    }
}

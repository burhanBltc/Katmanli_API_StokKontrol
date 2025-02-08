using _01_Stok.Entities.Models.Abstract;
using _01_Stok.Entities.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_Stok.Entities.Models.Concrete
{
    public class Order : BaseEntity
    {

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }

        public Status Status { get; set; }

        //bir orderId çokca orderdetailde gecebilir
        public List<OrderDetails> OrderDetails { get; set; }
        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

    }
}
using _01_Stok.Entities.Models.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_Stok.Entities.Models.Concrete
{
    public class OrderDetails :BaseEntity //satış tablosu
    {
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product Product { get; set; }


        public double UnitPrice { get; set; }   
        public int Quantity { get; set; }

    }
}
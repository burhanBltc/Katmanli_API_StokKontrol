using _01_Stok.Entities.Models.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_Stok.Entities.Models.Concrete
{
    public class Product : BaseEntity
    {
      
        public string ProductName { get; set; }
        public double Price { get; set; }
        public short? Stock { get; set; }

        public DateTime? ExpireDate { get; set; } //son kullanma, geçerlilik tarihi


        //navigation, ürünün 1 kategorisi 1 tedarikçisi var
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        public Supplier? Suplier { get; set; }

        //sipariş detaylarında çokça ürün kullanılabilir
        public List<OrderDetails> OrderDetails { get; set; }

        public Product()
        {
            OrderDetails = new List<OrderDetails>();
        }
    }
}












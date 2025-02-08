using _01_Stok.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Stok.Entities.Models.Concrete
{
    public class Supplier : BaseEntity
    {
        public string SupplierName { get; set; }    
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public List<Product> Products { get; set; }

        public Supplier()
        {
            Products = new List<Product>();
        }
    }
}

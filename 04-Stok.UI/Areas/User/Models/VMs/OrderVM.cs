using _01_Stok.Entities.Models.Concrete;
using Microsoft.Identity.Client;

namespace _04_Stok.UI.Areas.User.Models.VMs
{
    public class OrderVM
    {
        // seçim yapabilmesi için - aktif olan ürünlerime ihtiyacım var
        public List<Product> AvailableProducts { get; set; }

        public List<OrderDetailVM> OrderDetails { get; set; }


    }
}

using _01_Stok.Entities.Models.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _04_Stok.UI.Areas.Admin.Models.VMs
{
    public class ProductUpdateVM
    {
        public Product Product { get; set; }

        //categorilerim icin
        public List<SelectListItem> Categories { get; set; }

        //tedarikçilerim-supplier'lar icin
        public List<SelectListItem> Suppliers { get; set; }
    }
}

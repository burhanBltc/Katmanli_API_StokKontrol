using System.ComponentModel.DataAnnotations;

namespace _04_Stok.UI.Areas.Admin.Models.SupplierDTOs
{
    public class SupplierListDTO
    {
      
        public int ID { get; set; }

        public string SupplierName { get; set; }


        public bool IsActive { get; set; }

    }
}

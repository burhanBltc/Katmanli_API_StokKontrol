using System.ComponentModel.DataAnnotations;

namespace _04_Stok.UI.Areas.Admin.Models.SupplierDTOs
{
    public class SupplierCreateDTO
    {
        //[Key] 
        //public int ID { get; set; }

        [Required]
        [StringLength(100)] 
        public string SupplierName { get; set; }


        [Required]
        [StringLength(200)] 
        public string Address { get; set; }

        [Required]
        [Phone] 
        public string Phone { get; set; }

        [Required]
        [EmailAddress] 
        public string Email { get; set; }

        public bool IsActive { get; set; }
        //public DateTime AddedDate { get; set; } = DateTime.Now; 
       // public DateTime? ModifiedDate { get; set; }
    

    }
}

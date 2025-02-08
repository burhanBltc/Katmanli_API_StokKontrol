using _01_Stok.Entities.Models.Abstract;
using _01_Stok.Entities.Models.Enums;


namespace _01_Stok.Entities.Models.Concrete
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string? Photo { get; set; }
        public string? PhotoUrl { get; set; }


        public string Password { get; set; }
        public UserRole Role { get; set; }

        //bir kullanıcının çokca siparişi olabilir
        public List<Order> Orders { get; set; }
        public User() 
        { Orders = new List<Order>(); }


    }
}

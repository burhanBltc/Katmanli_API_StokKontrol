using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Stok.Entities.Models.Abstract
{
    public class BaseEntity
    {
        //fluentapiyle(hasone...) degil data anotationsla baglanıcam. Fluent daha sağlıklı veya birlikte daha önce yaptıgımız icin bu defa böyle yapıcaz

        //primary key
        [Column(Order =1)] //sql deki sıralama sırası, ilk sütun
        public int ID { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

    }
}

using _01_Stok.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Stok.Entities.Models.Concrete
{
    public class Category : BaseEntity
    {
        //default ta eager bırakmış oluyorum, include
        public string CategoryName { get; set; }
        public string Description { get; set; }


        public Category() //const ayaga kalkarken inst.listeyi olustur
        {
            Products = new List<Product>();
        }
        public List<Product>? Products { get; set; }

    }
}


/*
 Include metodu, Entity Framework'te Eager Loading için kullanılır. Eager Loading, ilişkili varlıkların hemen yüklenmesini sağlar. Lazy Loading ise ilişkili varlıkları yalnızca gerektiğinde yükler ve Include metodu bu durumda kullanılmaz.

Örneğin, Eager Loading ile ilişkili varlıkları yüklemek için:
var parent = context.Parents.Include(p => p.Children).FirstOrDefault(p => p.Id == id);

Lazy Loading kullanıyorsanız, ilişkili varlıklar otomatik olarak yüklenir ve Include metoduna gerek kalmaz. Lazy Loading'i etkinleştirmek için ilişkili varlıkları sanal (virtual) olarak tanımlamanız ve Lazy Loading proxy'lerini etkinleştirmeniz yeterlidir. Ayrıca, DbContext'inizi yapılandırırken LazyLoadingEnabled özelliğini true olarak ayarlamanız gerekir:
public class Parent
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Child> Children { get; set; }
}

public class MyDbContext : DbContext
{
    public MyDbContext()
    {this.Configuration.LazyLoadingEnabled = true;}

    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get



Başka bir yöntem olarak, Explicit Loading kullanabilirsiniz. Bu yöntemle, ilişkili varlıkları manuel olarak yükleyebilirsiniz:
var parent = context.Parents.FirstOrDefault(p => p.Id == id);
context.Entry(parent).Collection(p => p.Children).Load();

Bu yöntemlerle ilişkili varlıkları yükleyebilirsiniz.
 */
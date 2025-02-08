using _01_Stok.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Stok.Repositories.Context
{
    public class StokDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //veritabanı bağlantı ayarlarını yapılandırmak için
        {
            //ne söylüyorsam optionsBuilder'a söylüyorum
            optionsBuilder.UseSqlServer("Server=.;Database=StokYzl5101;Trusted_Connection=True;MultipleActiveResultSets=true; TrustServerCertificate=True;"); //migration icin burada verdik
        }

        //OnModelCreating metodu ise veritabanı modelini ve ilişkilerini tanımlamak için kullanılır. 

       public StokDbContext(DbContextOptions<StokDbContext> opt): base(opt) { } //bu defa iki yerde con.strng verdik, 1- migration icin OnConfiguring metodunda verdik, 2- migr sonra program.cs te yazdık, uygulamada repo/servisler icin
        //Bu kod parçası, Entity Framework Core kullanarak bir veritabanı bağlamı (DbContext) sınıfı oluşturmak için kullanılır. StokDbContext sınıfı, DbContext sınıfından türetilir ve DbContextOptions<StokDbContext> türünde bir seçenekler nesnesi alır. Bu seçenekler nesnesi, veritabanı bağlantı bilgilerini ve diğer yapılandırma seçeneklerini içerir. Kısacası, bu kod parçası, Entity Framework Core kullanarak veritabanı işlemlerini gerçekleştirmek için bir bağlam sınıfı oluşturur.Bu bağlam sınıfı, veritabanı bağlantı bilgilerini ve yapılandırma seçeneklerini alarak, veritabanı işlemlerini gerçekleştirmek için kullanılır


        /*
         * OnConfiguring metodu veritabanı bağlantı ayarlarını yapılandırmak için, 
         * OnModelCreating metodu ise veritabanı modelini ve ilişkilerini tanımlamak için kullanılır. 
         * Bu metotlar, EF Core ile çalışırken veritabanı bağlamınızı ve modelinizi özelleştirmenize olanak tanır.
         * 
         * Veritabanı Tabloları Arasındaki İlişkileri Tanımlama:
       protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { modelBuilder.Entity<YourEntity>() .HasOne(e => e.RelatedEntity) 
        .WithMany(re => re.YourEntities) .HasForeignKey(e => e.RelatedEntityId); }

        varlık özellik ve ilişkilerini tanımlama
        modelBuilder.Entity<YourEntity>() .HasKey(e => e.Id); 
        modelBuilder.Entity<YourEntity>() .Property(e => e.Name) .IsRequired();
         */






    }
}

using _01_Stok.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _02_Stok.Repositories.Repositories.Abstract
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        bool Add(T entity); //body sini concrete inde yazdık
        bool Add(List<T> items);
        bool Update(T entity);

        bool Remove(T entity);
        bool Remove(int id);
        bool Remove(Expression<Func<T, bool>> expression); //bool, lambda ifadesinin dönüş türünü belirtir. Bu ifade, T türündeki bir varlığı alır ve bir bool değeri döner. Başka bir deyişle, bu ifade, belirli bir koşulu sağlayıp sağlamadığını kontrol eder.

        T GetById(int id);
        T GetByDefault(Expression<Func<T, bool>> expression);
        IQueryable<T> GetById(int id, params Expression<Func<T, object>>[] includes); //ilişkileri ile tek kişi getir
        //IQueryable sorgulanabilir, kaç parametre bilmiyorum, kimleri include edecek
        List<T> GetActive(); //aktifleri getir
        IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes); //aktifleri iliskileri ile getir
        List<T> GetDefault(Expression<Func<T, bool>> expression); //şarta uyan herkes

        List<T> GetAll(); //hepsini getir, productları çekersen suplier'ı null gelir
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);//ilişkileri ile herkes gelsin, fiyatı 50 den büyük olanların supplier adıyla gelsin
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] exp);
        bool Activate(int id); //aktif et
        bool Any(Expression<Func<T, bool>> expression); //şartı sağlayan kimse var mı
        bool Save();

        //IQueryable<T> GetById(int id, params Expression<Func<T, object>>[] includes); metodu, belirli bir id değerine sahip olan bir varlığı ve onunla ilişkili diğer varlıkları sorgulamak için kullanılır. Bu metot, IQueryable<T> türünde bir sonuç döndürür, yani sorgulama işlemlerine devam edilebilir. includes burada bir dizi (array) parametresinin adıdır. params anahtar kelimesi kullanılarak tanımlanmış bir dizi olduğu için, bu parametreye birden fazla Expression<Func<T, object>> ifadesi geçebilirsiniz. Bu ifadeler, sorgulama sırasında hangi ilişkili varlıkların dahil edileceğini belirtir. var entity = repository.GetById(1, e => e.RelatedEntity1, e => e.RelatedEntity2);



        /*
         C# dilinde, bir erişim belirleyici belirtilmediğinde, varsayılan olarak private kabul edilir. Ancak, bu durum yalnızca sınıf üyeleri (fields, methods, properties, vb.) için geçerlidir.
        Interfaceler için ise, tüm üyeler varsayılan olarak public kabul edilir. Bu nedenle, IGenericRepo<T> interface'inde belirtilen Add ve Update metotları otomatik olarak public erişim belirleyicisine sahip olacaktır.*/









    }
}

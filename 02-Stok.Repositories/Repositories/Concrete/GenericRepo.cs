using _01_Stok.Entities.Models.Abstract;
using _02_Stok.Repositories.Context;
using _02_Stok.Repositories.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace _02_Stok.Repositories.Repositories.Concrete
{
    public class GenericRepo<T> : IGenericRepo<T> where T:BaseEntity
    {
        private readonly StokDbContext _context;

        public GenericRepo(StokDbContext context)
        {
            _context = context;
        }


        public T GetById(int id) => _context.Set<T>().Find(id);  //tek satırsa süslü artı return yerine lambda kullanabiliyoruz, { return } : =>

        public bool Activate(int id)
        {
            T item = GetById(id);
            item.IsActive = true;
            return Update(item);
        }

        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return Save();
        }

        public bool Add(List<T> items)
        {
            try
            {
                //yarım kalırsa
                //transaction *roolback ya da commit: hata yapılan işlemleri geri al ya da commitle onayla bitsin
                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (var item in items)
                    {
                        _context.Set<T>().Add(item);

                    }
                    ts.Complete();
                    return Save();
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Any(Expression<Func<T,bool>> expression) => _context.Set<T>().Any(expression);

        public List<T> GetActive() => _context.Set<T>().Where(x => x.IsActive).ToList();


        public IQueryable<T> GetActive(params Expression<Func<T,object>>[] includes) //eager loadin, iliskileri birlestirip
        {
            var query = _context.Set<T>().Where(a => a.IsActive);//active olan IQuery tipleri
            query = includes.Aggregate(query, (current, include) => current.Include(include));//query baslangıc, current üzerine (includes ları) include ile eklenmiş hali
            //query = query.include(category).include(supplier).include(orderdetail)
            return query;
        }

        public List<T> GetAll() => _context.Set<T>().ToList();


        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query; //herkes iliskileriyle gelsin, filtresiz

        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] exp)
        {
            var query = _context.Set<T>().Where(expression);
            query = exp.Aggregate(query, (current, include) => current.Include(include));
            return query; //herkes iliskileriyle gelsin ama belli bir filtreleme ile
        }

  



        public T GetByDefault(Expression<Func<T, bool>> expression) => _context.Set<T>().FirstOrDefault(expression);


        public IQueryable<T> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(a => a.ID == id);
            query = includes.Aggregate(query, (current, includes) => current.Include(includes));
            return query; //id si bildigim birini herşeyiyle
        }

        public List<T> GetDefault(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression).ToList();


        public bool Remove(T entity)
        {
            entity.IsActive = false;
            return Update(entity);
        }

        public bool Remove(int id)
        {
            //id de biri yok hata atabilir
            try
            {
                T item = GetById(id);
                return Remove(item);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(Expression<Func<T, bool>> expression)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var collection = GetDefault(expression);
                    foreach (var item in collection)
                    {
                        item.IsActive = false;
                        Update(item);
                    }
                    ts.Complete();
                    return true;
                }
            }
            catch (Exception)
            {
                return false; //ts rollback le yaptıklarını geri alacak
            }
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            try
            {
            entity.ModifiedDate = DateTime.Now;
                _context.Set<T>().Update(entity);
                return Save();
            }
            catch (Exception)
            {
                return false; 
            }
        }
    }
}

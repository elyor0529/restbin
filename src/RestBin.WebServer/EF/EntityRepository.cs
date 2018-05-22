using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RestBin.WebServer.EF
{
    public interface IEntityRepository<T> : IDisposable
    {

        IEnumerable<T> GetAll();

        IQueryable<T> Where(Expression<Func<T, bool>> pred);

        T Find(Expression<Func<T, bool>> pred);

        T GetById(int id);

        void Add(T t);

        void Edit(T t);

        void Remove(int id);

        int Save();

    }

    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly AppDbContext _db = new AppDbContext();

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().AsEnumerable();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> pred)
        {
            return _db.Set<T>().Where(pred);
        }

        public T Find(Expression<Func<T, bool>> pred)
        {
            return _db.Set<T>().FirstOrDefault(pred);
        }

        public T GetById(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public void Add(T t)
        {
            _db.Set<T>().Add(t);
            _db.Entry(t).State = EntityState.Added;
        }

        public void Edit(T t)
        {
            _db.Set<T>().Attach(t);
            _db.Entry(t).State = EntityState.Modified;
        }

        public void Remove(int id)
        {
            var t = _db.Set<T>().Find(id);

            _db.Entry(t).State = EntityState.Deleted;
        }

        public int Save()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}

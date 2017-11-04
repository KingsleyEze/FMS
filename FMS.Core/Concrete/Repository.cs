using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FMS.Core.Abstract;

namespace FMS.Core.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> Items => _dbSet;

        public IQueryable<T> AsUntrackedItems => _dbSet.AsNoTracking<T>();
        public void Insert(T obj) => _dbSet.Add(obj);

        public void Insert(IEnumerable<T> obj) => _dbSet.AddRange(obj);

        public void Update(T obj) => _dbSet.Update(obj);

        public void Update(IEnumerable<T> obj) => _dbSet.UpdateRange(obj);

        public void Delete(T obj) => _dbSet.Remove(obj);

        public void Delete(IEnumerable<T> obj) => _dbSet.RemoveRange(obj);
        public IQueryable<T> SelectRaw(string rawSql, object[] param = null) => Items.FromSql(rawSql, param);

    }
}

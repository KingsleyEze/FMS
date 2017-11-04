using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FMS.Core.Abstract
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Items { get; }
        IQueryable<T> AsUntrackedItems { get; }
        void Insert(T obj);
        void Insert(IEnumerable<T> obj);
        void Update(T obj);
        void Update(IEnumerable<T> obj);
        void Delete(IEnumerable<T> objects);
        void Delete(T objects);
        IQueryable<T> SelectRaw(string rawSql, object[] param = null);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository<T>
    {
      
        IQueryable<T> GetAll();
        void RemoveRange(IEnumerable<string> ids);
       
        T GetById(string id);
       
        T Add(T entity);
        IEnumerable<T> EditRange(IEnumerable<T> entityList);
        IEnumerable<T> AddRange(IEnumerable<T> entityList);
        T Edit(T entity);
        void Remove(string id);
        
        IQueryable<T> Find(Expression<Func<T, bool>> where);
        T First(Expression<Func<T, bool>> where);

        T SaveIncluded(T entity, params string[] includedProperties);
        T SaveExcluded(T entity, params string[] excludedProperties);
        void DeleteRange(IEnumerable<T> entities);
        void Delete(string id);

    }
}

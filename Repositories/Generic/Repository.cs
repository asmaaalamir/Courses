using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        public readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            _dbSet = context.Set<T>();
        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
        {
            IQueryable<T> query = _dbSet.Where(x => !x.IsDeleted);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!String.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).AsQueryable();
            }
            else
            {
                return query.AsQueryable();
            }
        }


        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }
        public virtual IEnumerable<T> AddRange(IEnumerable<T> entityList)
        {
            var list = new List<T>();
            foreach (T entity in entityList)
                list.Add(Add(entity));
            return list;
        }
        public virtual IEnumerable<T> EditRange(IEnumerable<T> entityList)
        {
            foreach (T entity in entityList)
                Edit(entity);
            return entityList;
        }
        public virtual T Edit(T entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return entity;
        }
        public virtual void Remove(string id)
        {
            T entity = _dbSet.Where(x => !x.IsDeleted).FirstOrDefault(i => i.ID == id);
            
            entity.IsDeleted = true;
          
            _context.Entry<T>(entity).State = EntityState.Modified;
        }
        public virtual void Delete(string id)
        {
            T entity = _dbSet.FirstOrDefault(i => i.ID == id);
            _dbSet.Remove(entity);
        }
        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);

        }
        public virtual void RemoveRange(IEnumerable<string> Ids)
        {
            foreach (string id in Ids)
                Remove(id);
        }
        public virtual T GetById(string id)
        {
            return _dbSet.FirstOrDefault(entity => entity.ID == id);
        }
      public virtual T SaveExcluded(T entity, params string[] excludedProperties)
        {
            if (excludedProperties.Any())
            {

                var oldEntity = _context.Set<T>().Local.FirstOrDefault(e => e.ID == entity.ID);
                if (oldEntity != null)
                    _context.Entry<T>(oldEntity).State = EntityState.Detached;

                _dbSet.Attach(entity);
               
                foreach (var name in excludedProperties)
                {
                    _context.Entry(entity).Property(name).IsModified = false;
                }
                var takenProp = _context.Entry<T>(entity).CurrentValues.Properties.Select(x => x.Name).Except(excludedProperties).Except(new[] { "ID" });
                foreach (var name in takenProp)
                {

                    _context.Entry(entity).Property(name).IsModified = true;
                }
               
            }
           
            return entity;
        }
        public virtual T SaveIncluded(T entity, params string[] included)
        {
            if (included.Any())
            {
                var oldEntity = _context.Set<T>().Local.FirstOrDefault(e => e.ID == entity.ID);
                if (oldEntity != null)
                    _context.Entry<T>(oldEntity).State = EntityState.Detached;

                _dbSet.Attach(entity);
                foreach (var name in included)
                {
                    _context.Entry(entity).Property(name).IsModified = true;
                }
                var excludedProps = _context.Entry<T>(entity).CurrentValues.Properties.Select(x => x.Name).Except(included);
                foreach (var name in excludedProps)
                {
                    _context.Entry(entity).Property(name).IsModified = false;
                }
               
            }
            return entity;
        }
        public virtual IQueryable<T> GetAll()
        {
            var query = _dbSet.Where(x => !x.IsDeleted);
            return query;
        }
        
        public virtual IQueryable<T> Find(Expression<Func<T, bool>> @where)
        {
            return _dbSet.Where(x => !x.IsDeleted).Where(@where);
        }
       
        public virtual IQueryable<T> Skip(Expression<Func<T, bool>> order, int skipRows, int? takenRows)
        {
            return takenRows == null ? _dbSet.Where(x => !x.IsDeleted).OrderBy(order).Skip(skipRows) :
             _dbSet.Where(x => !x.IsDeleted).OrderBy(order).Skip(skipRows).Take(takenRows.Value);
        }
        public virtual T First(Expression<Func<T, bool>> @where)
        {
            var query = _dbSet.Where(x => !x.IsDeleted).Where(@where);
            return query.FirstOrDefault();
        }
    }
}


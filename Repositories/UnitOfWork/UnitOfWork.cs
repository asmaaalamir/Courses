
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private readonly IServiceProvider _serviceProvider;
        public UnitOfWork(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            this.context = context;
            _serviceProvider = serviceProvider;

        }
        public T Repository<T>() where T : class

        {
            var interfaceType = typeof(T).GetInterface($"I{typeof(T).Name}");

            T repo = (T)_serviceProvider.GetRequiredService(interfaceType);

            return repo;
        }
        public void Commit()
        {
            context.SaveChanges();
        }
        public void Rollback()
        {
            context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
        public IDbContextTransaction BeginTransaction()
        {
            IDbContextTransaction res = context.Database.BeginTransaction();
            return res;
        }
    }
}

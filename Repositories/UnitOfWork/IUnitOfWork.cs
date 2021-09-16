using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
   public interface IUnitOfWork
    {
        T Repository<T>() where T : class;
        void Commit();
        void Rollback();
        IDbContextTransaction BeginTransaction();
    }
}

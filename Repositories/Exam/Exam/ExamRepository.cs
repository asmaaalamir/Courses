
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class ExamRepository : Repository<Exam>, IExamRepository
    {
        public ExamRepository(ApplicationDbContext context):base(context)
        {
        }
    }
}

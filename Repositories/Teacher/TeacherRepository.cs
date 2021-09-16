
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext context):base(context)
        {
        }
    }
}

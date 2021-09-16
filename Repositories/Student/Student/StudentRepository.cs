
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context):base(context)
        {
        }
    }
}

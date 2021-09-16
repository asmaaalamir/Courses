
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class StudentCourseRepository : Repository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseRepository(ApplicationDbContext context):base(context)
        {
        }
    }
}

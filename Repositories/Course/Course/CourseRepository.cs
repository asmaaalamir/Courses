
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context):base(context)
        {
        }
    }
}

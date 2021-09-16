
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(ApplicationDbContext context):base(context)
        {
        }
    }
}

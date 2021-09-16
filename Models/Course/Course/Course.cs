using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Course")]
    public class Course :BaseModel
    {
        public string Name { get; set; }
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("Teacher")]
        public string TeacherID { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        
    }
}

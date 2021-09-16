using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Student")]
    public class Student :BaseModel
    {
        [ForeignKey("User")]
        public string UserID { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }


    }
}

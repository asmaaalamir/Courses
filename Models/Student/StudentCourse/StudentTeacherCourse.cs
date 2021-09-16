using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("StudentCourse")]
    public class StudentCourse :BaseModel
    {
        public virtual Student Student { get; set; }
        [ForeignKey("Student")]
        public string StudentID { get; set; }
        public virtual Course Course { get; set; }
        [ForeignKey("Course")]
        public string CourseID { get; set; }



    }
}

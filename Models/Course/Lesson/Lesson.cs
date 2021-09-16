using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Lesson")]
    public class Lesson : BaseModel
    {
       
        public string Name { get; set; }
        public virtual Course Course { get; set; }
        [ForeignKey("Course")]
        public string CourseID { get; set; }

    }
}

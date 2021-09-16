using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Teacher")]
    public class Teacher :BaseModel
    {
        
        [ForeignKey("User")]
        public  string  UserID{ get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

    }
}

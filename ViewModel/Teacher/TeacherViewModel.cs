using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ViewModel
{

    public class TeacherViewModel
    {
        public string ID { get; set; }
        public IdentityUser User { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}

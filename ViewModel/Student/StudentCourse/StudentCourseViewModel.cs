using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ViewModel
{

    public class StudentCourseViewModel
    {
        public string ID { get; set; }
        public string StudentID { get; set; }
        public string CourseID { get; set; }

    }
}

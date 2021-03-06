using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ViewModel
{
    
    public class CourseEditViewModel
    {
        public string ID{get; set; }
        public string Name { get; set; }
        public string TeacherID { get; set; }
        public List<LessonEditViewModel> Lessons { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ViewModel
{
    
    public class ExamEditViewModel
    {
        public string ID{get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CourseID { get; set; }
    }
}

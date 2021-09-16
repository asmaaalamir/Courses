using AutoMapper;
using Models;

namespace ViewModel
{
    public class StudentCourseProfile : Profile
    {
        public StudentCourseProfile()
        {
            CreateMap<StudentCourseEditViewModel, StudentCourse>(MemberList.None);
            CreateMap<StudentCourse, StudentCourseViewModel>(MemberList.None);
     
          
        }
    }
 
}

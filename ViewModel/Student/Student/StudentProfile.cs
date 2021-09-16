using AutoMapper;
using Models;
using System.Linq;

namespace ViewModel
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentEditViewModel, Student>(MemberList.None)
               .ForMember(x => x.StudentCourses, y => y.Ignore());
            CreateMap<Student, StudentViewModel>(MemberList.None)
                .ForMember(x=>x.StudentCourses, y=>y.Ignore());



            CreateMap<Student, StudentViewModel>().AfterMap(
                            (src, dest, c) =>
                            {
                                dest.StudentCourses = src?.StudentCourses?.Where(x => !x.IsDeleted).Select(x => Mapper.Map<StudentCourseViewModel>(x)).ToList();

                            }
             );

            CreateMap<Student, StudentEditViewModel>().AfterMap(
                            (src, dest, c) =>
                            {
                                dest.StudentCourses = src?.StudentCourses?.Where(x => !x.IsDeleted).Select(x => Mapper.Map<StudentCourseEditViewModel>(x)).ToList();

                            }
             );

        }
    }
 
}

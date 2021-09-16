using AutoMapper;
using Models;
using System.Linq;

namespace ViewModel
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<TeacherEditViewModel, Teacher>(MemberList.None)
               .ForMember(x => x.Courses, y => y.Ignore());
            CreateMap<Teacher, TeacherViewModel>(MemberList.None)
                .ForMember(x=>x.Courses, y=>y.Ignore());



            CreateMap<Teacher, TeacherViewModel>().AfterMap(
                            (src, dest, c) =>
                            {
                                dest.Courses = src?.Courses?.Where(x => !x.IsDeleted).Select(x => Mapper.Map<CourseViewModel>(x)).ToList();

                            }
             );

            //CreateMap<Teacher, TeacherEditViewModel>().AfterMap(
            //                (src, dest, c) =>
            //                {
            //                    dest.Courses = src?.Courses?.Where(x => !x.IsDeleted).Select(x => Mapper.Map<CourseEditViewModel>(x)).ToList();

            //                }
            // );

        }
    }
 
}

using AutoMapper;
using Models;
using System.Linq;

namespace ViewModel
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseEditViewModel, Course>(MemberList.None)
                .ForMember(x => x.Lessons, y => y.Ignore());
            CreateMap<Course, CourseViewModel>(MemberList.None)
                 .ForMember(x => x.Lessons, y => y.Ignore());
               


            CreateMap<Course, CourseViewModel>().AfterMap(
                            (src, dest, c) =>
                            {
                                dest.Lessons = src?.Lessons?.Where(x => !x.IsDeleted).Select(x => Mapper.Map<LessonViewModel>(x)).ToList();

                            }
             );

            CreateMap<Course, CourseEditViewModel>().AfterMap(
                            (src, dest, c) =>
                            {
                                dest.Lessons = src?.Lessons?.Where(x => !x.IsDeleted).Select(x => Mapper.Map<LessonEditViewModel>(x)).ToList();

                            }
             );

        }
    }
 
}

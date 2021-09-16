using AutoMapper;
using Models;

namespace ViewModel
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<LessonEditViewModel, Lesson>(MemberList.None);
            CreateMap<Lesson, LessonViewModel>(MemberList.None);
          
        }
    }
 
}

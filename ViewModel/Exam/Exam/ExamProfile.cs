using AutoMapper;
using Models;

namespace ViewModel
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<ExamEditViewModel, Exam>(MemberList.None);
            CreateMap<Exam, ExamViewModel>(MemberList.None);
          
        }
    }
 
}

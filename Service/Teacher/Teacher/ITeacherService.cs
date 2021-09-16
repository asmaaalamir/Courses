

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service
{
    public interface ITeacherService : IBaseService<TeacherEditViewModel, TeacherViewModel>
    {
        PagingViewModel Get(string name = "", string orderBy = "ID", bool isAscending = false, int pTeacherIndex = 1, int pTeacherSize = 20);
        Task<TeacherEditViewModel> AddTeacher(TeacherEditViewModel model);
        Task<TeacherEditViewModel> EditTeacher(TeacherEditViewModel model);
        Task<Object> Login(LoginViewModel model);
    }
}

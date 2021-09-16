

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service
{
    public interface IStudentService : IBaseService<StudentEditViewModel, StudentViewModel>
    {
        PagingViewModel Get(string name = "", string orderBy = "ID", bool isAscending = false, int pStudentIndex = 1, int pStudentSize = 20);
        Task<StudentEditViewModel> AddStudent(StudentEditViewModel model);
        Task<StudentEditViewModel> EditStudent(StudentEditViewModel model);
        Task<Object> Login(LoginViewModel model);
        List<string> GetStudents(string CourseID);
    }
}



using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace Service
{
    public interface ILessonService : IBaseService<LessonEditViewModel, LessonViewModel>
    {
        PagingViewModel Get(string name = "", string orderBy = "ID", bool isAscending = false, int pageIndex = 1, int pageSize = 20);
    }
}

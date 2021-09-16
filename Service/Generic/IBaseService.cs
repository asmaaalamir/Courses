
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IBaseService<TEditViewModel, TViewModel> 
    {
        IEnumerable<TViewModel> GetList();
        TViewModel GetByID(string id);
        TEditViewModel GetEditableByID(string id);
        TEditViewModel Add(TEditViewModel model);
        TEditViewModel Edit(TEditViewModel model);
        void Remove(string id);
    }
}

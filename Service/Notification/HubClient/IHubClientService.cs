
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace Service
{
    public interface IHubClientService : IBaseService<HubClientEditViewModel, HubClientViewModel>
    {
        void Remove(string connectionID);
        List<HubClientViewModel> Get(string UserID);
        List<HubClientViewModel> GetByToken(string token);
    }
}

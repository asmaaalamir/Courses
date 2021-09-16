
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace Service
{
    public interface INotificationService : IBaseService<NotificationEditViewModel, NotificationViewModel>
    {
        List<NotificationViewModel> GetList( bool allNotification, string UserID);

        bool UpdateRead(List<string> Ids);

        int GetUnReadCount(string UserID);

    }
}

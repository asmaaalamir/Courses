
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using ViewModel;
using Repositories;

namespace Service
{
    public class NotificationService :GenericService<Notification, NotificationEditViewModel, NotificationViewModel>, 
        INotificationService
    {
        private readonly INotificationRepository _NotificationRepository;
        public NotificationService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
            _NotificationRepository = _unitOfWork.Repository<NotificationRepository>();
        }
        public List<NotificationViewModel> GetList( bool allNotification,string UserID)
        {
            if (allNotification)
                return _NotificationRepository.Find(item => !item.IsDeleted &&
                item.UserID == UserID).ToList()
                .Select(outerItem => Mapper.Map<NotificationViewModel>(outerItem))
                .OrderByDescending(x => x.ID).ToList();

            else
                return _NotificationRepository.Find(item => 
                !item.IsDeleted && item.UserID == UserID 
               )
                    .OrderByDescending(x => x.ID).Take(10).ToList()
                    .Select(outerItem => Mapper.Map< NotificationViewModel >( outerItem)).ToList();
        }
        public int GetUnReadCount(string UserID)
        {
            return _NotificationRepository.Find(item => !item.IsDeleted &&
            item.UserID == UserID 
           && !item.IsRead).Count();
        }
        public bool UpdateRead(List<string> Ids)
        {
            foreach (var id in Ids)
            {
                _NotificationRepository.SaveIncluded(new Notification() { ID = id, IsRead = true }, "IsRead");
                _unitOfWork.Commit();
            }

            return true;
        }



    }
}

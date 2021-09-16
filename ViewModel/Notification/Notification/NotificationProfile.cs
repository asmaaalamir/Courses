using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace ViewModel
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationEditViewModel, Notification>(MemberList.None);
            CreateMap<Notification, NotificationViewModel>().AfterMap(
                            (src, dest, c) =>
                            {
                            }
                            );
        }
    }
}

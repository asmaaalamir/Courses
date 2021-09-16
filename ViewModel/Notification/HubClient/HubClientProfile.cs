using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace ViewModel
{
    public class HubClientProfile : Profile
    {
        public HubClientProfile()
        {
            CreateMap<HubClientEditViewModel, HubClient>(MemberList.None);
            CreateMap<HubClient, HubClientViewModel>().AfterMap(
                            (src, dest, c) =>
                            {
                            }
                            );
        }
    }
}

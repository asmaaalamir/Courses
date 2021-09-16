
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
    public class HubClientService :GenericService<HubClient, HubClientEditViewModel, HubClientViewModel>, IHubClientService
    {
        private readonly IHubClientRepository _HubClientRepository;
        public HubClientService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
            _HubClientRepository = _unitOfWork.Repository<HubClientRepository>();
        }


        public List<HubClientViewModel> Get(string UserID)
        {
            var item = _HubClientRepository.Find(x => !x.IsDeleted && x.UserID == UserID)
                .OrderByDescending(x => x.ID).ToList();


            if (item == null) return null;
            var itemViewModel = item.Select(x=> Mapper.Map<HubClientViewModel>(x)).ToList();
            return itemViewModel;
        }

        public List<HubClientViewModel> GetByToken(string token)
        {
            var item = _HubClientRepository.Find(x => !x.IsDeleted && x.ConnectionId == token).OrderByDescending(x => x.ID).ToList();


            if (item == null) return null;
            var itemViewModel = item.Select(x => Mapper.Map<HubClientViewModel>(x)).ToList();
            return itemViewModel;
        }

        public void Remove(string connectionID)
        {
            var item = _repository.Find(x => !x.IsDeleted && x.ConnectionId == connectionID).FirstOrDefault();
            if(item != null)
            _HubClientRepository.Delete(item.ID);
            _unitOfWork.Commit();
        }

    }
}

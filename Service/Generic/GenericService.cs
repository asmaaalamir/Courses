using AutoMapper;
using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class GenericService<TModel, TEditViewModel, TViewModel> : IBaseService<TEditViewModel, TViewModel> where TModel : BaseModel
    {
        protected IUnitOfWork _unitOfWork;
        protected IRepository<TModel> _repository;
        public GenericService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _repository = unitOfWork.Repository<Repository<TModel>>();
        }
        public virtual TEditViewModel Add(TEditViewModel model)
        {
            var obj = Mapper.Map<TModel>(model);
            var attachedObj = _repository.Add(obj);
            _unitOfWork.Commit();
            return Mapper.Map<TEditViewModel>(attachedObj);
        }
        public virtual TEditViewModel Edit(TEditViewModel model)
        {
            var obj = Mapper.Map<TModel>(model);
            var attachedObj = _repository.Edit(obj);
            _unitOfWork.Commit();
            return Mapper.Map<TEditViewModel>(attachedObj);
        }
        public virtual TViewModel GetByID(string id)
        {
            var model = _repository.First(x => x.ID == id);
            return Mapper.Map<TViewModel>(model);
        }
        public virtual TEditViewModel GetEditableByID(string id)
        {
            var city = _repository.First(x => x.ID == id);
            return Mapper.Map<TEditViewModel>(city);
        }
        public virtual IEnumerable<TViewModel> GetList()
        {

            return _repository.GetAll().ToList().Select(x => Mapper.Map<TViewModel>(x)).ToList();
        }
        public virtual void Remove(string id)
        {
            _repository.Remove(id);
            _unitOfWork.Commit();
        }
    }
}

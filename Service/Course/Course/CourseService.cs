
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;
using ViewModel;

namespace Service
{
    public class CourseService : GenericService<Course,CourseEditViewModel,CourseViewModel>,  ICourseService
    {
        private readonly ICourseRepository _CourseRepository;
        private readonly ILessonRepository _LessonRepository;
        public CourseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
           
            _CourseRepository = (ICourseRepository) unitOfWork.Repository<CourseRepository>();
            _LessonRepository = (ILessonRepository)unitOfWork.Repository<LessonRepository>();
        }
        public PagingViewModel Get(string name = "", string orderBy = "ID", bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var list = _CourseRepository.GetAll();
            //if (!string.IsNullOrEmpty(name))
            //    list = list.Where(x => x.Name.Contains(name));
            int records = list.Count();
            if (records <= pageSize || pageIndex <= 0) pageIndex = 1;
            int pages = (int)Math.Ceiling((double)records / pageSize);
            int excludedRows = (pageIndex - 1) * pageSize;
            IEnumerable<CourseViewModel> result = new List<CourseViewModel>();
            var items = list.OrderByDescending(x=>x.ID).Skip(excludedRows).Take(pageSize);
            result = items.ToList().Select(x => Mapper.Map<CourseViewModel>(x)).ToList();
            return new PagingViewModel() { PageIndex = pageIndex, PageSize = pageSize, Result = result, Records = records, Pages = pages };
        }

        public override CourseEditViewModel Add(CourseEditViewModel model)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                     if(IsExist(model.ID,model.Name))
                        throw new Exception("Already Exists");
                    var obj = Mapper.Map<Course>(model);
                    var attachedObj = _repository.Add(obj);
                    _unitOfWork.Commit();

                    if (model.Lessons != null && model.Lessons.Count > 0)
                    {
                        foreach (var i in model.Lessons)
                            i.CourseID = attachedObj.ID;

                    }
                    _LessonRepository.AddRange(model.Lessons.Select(i => Mapper.Map<Lesson>(i)));
                    _unitOfWork.Commit();
                    var CourseEditViewModel = Mapper.Map<CourseEditViewModel>(attachedObj);
                    transaction.Commit();

                    return CourseEditViewModel;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public override CourseEditViewModel Edit(CourseEditViewModel model)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    if (IsExist(model.ID, model.Name))
                        throw new Exception("Already Exists");
                    var obj = Mapper.Map<Course>(model);
                     var attachedObj = _repository.Edit(obj);
                   
                    _unitOfWork.Commit();
                    if (model.Lessons != null && model.Lessons.Count > 0)
                    {
                        foreach (var item in model.Lessons)
                        {
                            
                            item.CourseID = obj.ID;
                        }

                        var list = model.Lessons.Select(i => Mapper.Map<Lesson>(i)); ;
                        string[] ids = list.Select(i => i.ID).ToArray();
                        var Lessons = _LessonRepository.Find(i => i.CourseID == obj.ID && !ids.Contains(i.ID));
                        _LessonRepository.AddRange(list.Where(i => i.ID == null));
                        _LessonRepository.EditRange(list.Where(i => i.ID != null));
                        _LessonRepository.RemoveRange(Lessons.Select(i => i.ID).ToList());
                       
                       
                    }
                    _unitOfWork.Commit();

                    transaction.Commit();
                    return Mapper.Map<CourseEditViewModel>(attachedObj);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public bool IsExist(string id, string name)
        {

            return _repository.GetAll().Any(x => x.Name == name && x.ID!=id && !x.IsDeleted);
        }
    }
}

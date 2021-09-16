
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
    public class LessonService : GenericService<Lesson,LessonEditViewModel,LessonViewModel>,  ILessonService
    {
        private readonly ILessonRepository _LessonRepository;
       
        public LessonService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
           
            _LessonRepository = (ILessonRepository) unitOfWork.Repository<LessonRepository>();
          
        }
        public PagingViewModel Get(string name = "", string orderBy = "ID", bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var list = _LessonRepository.GetAll();
            //if (!string.IsNullOrEmpty(name))
            //    list = list.Where(x => x.Name.Contains(name));
            int records = list.Count();
            if (records <= pageSize || pageIndex <= 0) pageIndex = 1;
            int pages = (int)Math.Ceiling((double)records / pageSize);
            int excludedRows = (pageIndex - 1) * pageSize;
            IEnumerable<LessonViewModel> result = new List<LessonViewModel>();
            var items = list.OrderByDescending(x=>x.ID).Skip(excludedRows).Take(pageSize);
            result = items.ToList().Select(x => Mapper.Map<LessonViewModel>(x)).ToList();
            return new PagingViewModel() { PageIndex = pageIndex, PageSize = pageSize, Result = result, Records = records, Pages = pages };
        }



    }
}


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
    public class ExamService : GenericService<Exam,ExamEditViewModel,ExamViewModel>,  IExamService
    {
        private readonly IExamRepository _ExamRepository;
       
        public ExamService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
           
            _ExamRepository = (IExamRepository) unitOfWork.Repository<ExamRepository>();
          
        }
        public PagingViewModel Get(string name = "", string orderBy = "ID", bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var list = _ExamRepository.GetAll();
            //if (!string.IsNullOrEmpty(name))
            //    list = list.Where(x => x.Name.Contains(name));
            int records = list.Count();
            if (records <= pageSize || pageIndex <= 0) pageIndex = 1;
            int pages = (int)Math.Ceiling((double)records / pageSize);
            int excludedRows = (pageIndex - 1) * pageSize;
            IEnumerable<ExamViewModel> result = new List<ExamViewModel>();
            var items = list.OrderByDescending(x=>x.ID).Skip(excludedRows).Take(pageSize);
            result = items.ToList().Select(x => Mapper.Map<ExamViewModel>(x)).ToList();
            return new PagingViewModel() { PageIndex = pageIndex, PageSize = pageSize, Result = result, Records = records, Pages = pages };
        }



    }
}

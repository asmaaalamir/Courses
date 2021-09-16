
using Microsoft.AspNetCore.Mvc;
using System;
using Service;
using ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Course.Controllers
{

    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class CourseController : ControllerBase
    {
        private ResultViewModel _resultViewModel;
        private readonly ICourseService _service;
        private readonly NotificationHub _NotificationHub;
        public CourseController(ICourseService service, NotificationHub NotificationHub)
        {
            _service = service;
            _resultViewModel = new ResultViewModel();
            _NotificationHub = NotificationHub;
        }
        
        [HttpGet]
        [Route("Get")]
        public ResultViewModel Get(string name = "", string orderBy = "ID", bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                _resultViewModel.Data = _service.Get(name, orderBy, isAscending, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
               _resultViewModel =  _resultViewModel.Create(false, ex.Message);
            }
            return _resultViewModel;

        }
       
        [HttpGet]
        [Route("GetList")]
        public ResultViewModel GetList()
        {

            try
            {
                _resultViewModel.Data =_service.GetList();
            }
            catch (Exception ex)
            {
               _resultViewModel =  _resultViewModel.Create(false, ex.Message);
            }
            return _resultViewModel;
        }
      
        [HttpGet]
        [Route("GetByID/{id}")]
        public ResultViewModel GetByID(string id)
        {
            try
            {
                _resultViewModel.Data = _service.GetByID(id);
            }
            catch (Exception ex)
            {
               _resultViewModel =  _resultViewModel.Create(false, ex.Message);
            }
            return _resultViewModel;
        }
        
        [HttpGet]
        [Route("GetEditableByID/{id}")]
        public ResultViewModel GetEditableByID(string id)
        {

            try
            {
                _resultViewModel.Data = _service.GetEditableByID(id);
            }
            catch (Exception ex)
            {
               _resultViewModel =  _resultViewModel.Create(false, ex.Message);
            }
            return _resultViewModel;
        }
        
        [HttpPost]
        [Route("Post")]
        public ResultViewModel Post([FromBody] CourseEditViewModel viewModel)
        {
            try
            {
                _resultViewModel = _resultViewModel.Create(true, "Successfully Created", _service.Add(viewModel));
               
            }
            catch (Exception ex)
            {
               _resultViewModel =  _resultViewModel.Create(false, ex.Message);
            }
            return _resultViewModel;
        }
        
        [HttpPut]
        [Route("Put")]
        public ResultViewModel Put([FromBody] CourseEditViewModel viewModel)
        {
            try
            {
                _resultViewModel = _resultViewModel.Create(true, "Successfully Updated", _service.Edit(viewModel));
                _NotificationHub.UpdateCourse(viewModel);
            }
            catch (Exception ex)
            {
               _resultViewModel =  _resultViewModel.Create(false, ex.Message);
            }
            return _resultViewModel;
        }
        
        [HttpDelete]
        [Route("Delete/{id}")]
        public ResultViewModel Delete(string id)
        {

            try
            {
                _service.Remove(id);
                _resultViewModel.Message = "Successfully Deleted";
            }
            catch (Exception ex)
            {
               _resultViewModel =  _resultViewModel.Create(false, ex.Message);
            }
            return _resultViewModel;
        }
    }
}

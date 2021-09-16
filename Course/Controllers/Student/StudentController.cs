
using Microsoft.AspNetCore.Mvc;
using System;
using Service;
using ViewModel;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Course.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private ResultViewModel _resultViewModel;

        private readonly IStudentService _service;
        private readonly ApplicationSettings _appSettings;
        public StudentController(IStudentService service)
                    
        {
            _service = service;
            _resultViewModel = new ResultViewModel();
          
        }
       //[Authorize]
        [HttpGet]
        [Route("Get")]
        public ResultViewModel Get(string name = "", string orderBy = "ID", bool isAscending = false, int pStudentIndex = 1, int pStudentSize = 20)
        {
            try
            {
                _resultViewModel.Data = _service.Get(name, orderBy, isAscending, pStudentIndex, pStudentSize);
            }
            catch (Exception ex)
            {
               _resultViewModel =  _resultViewModel.Create(false, ex.Message);
            }
            return _resultViewModel;

        }
       //[Authorize]
        [HttpGet]
        [Route("GetList")]
        public ResultViewModel GetList()
        {

            try
            {
                _resultViewModel.Data = _service.GetList();
            }
            catch (Exception ex)
            {
               _resultViewModel =  _resultViewModel.Create(false, ex.Message);
            }
            return _resultViewModel;
        }
       //[Authorize]
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
       //[Authorize]
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
        public async Task<ResultViewModel> Post([FromBody] StudentEditViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {           
                        _resultViewModel = _resultViewModel.Create(true, "Successfully Created", await _service.AddStudent(model));                  
                }
               
            }
            catch (Exception ex)
            {
               _resultViewModel =  _resultViewModel.Create(false, ex.Message);
            }
            return _resultViewModel;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<ResultViewModel> Login([FromBody] LoginViewModel model)
        {
            try
            {
                _resultViewModel = _resultViewModel.Create(true, "Successfully Login", await _service.Login(model));
            }
            catch (Exception ex)
            {
                _resultViewModel = _resultViewModel.Create(false, "user Name Or password not scuccess");
            }
          
            return _resultViewModel;
        }
       //[Authorize]
        [HttpPut]
        [Route("Put")]
        public async Task<ResultViewModel> Put([FromBody] StudentEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {            
                    _resultViewModel = _resultViewModel.Create(true, "Successfully Updated",await _service.EditStudent(model));

                }

            }
            catch (Exception ex)
            {
                _resultViewModel = _resultViewModel.Create(false, ex.Message);
            }
            return _resultViewModel;
           
            
        }
       //[Authorize]
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

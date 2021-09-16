using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;
using ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;

namespace Service
{
    public class StudentService : GenericService<Student,StudentEditViewModel,StudentViewModel>,  IStudentService
    {
        private readonly IStudentRepository _StudentRepository;
        private readonly IStudentCourseRepository _StudentCourseRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private readonly IConfiguration configuration;
        public StudentService(IUnitOfWork unitOfWork,
             UserManager<IdentityUser> userManager,
            IOptions<ApplicationSettings> appSettings,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager) : base(unitOfWork)
        {
           
            _StudentRepository = (IStudentRepository) unitOfWork.Repository<StudentRepository>();
            _StudentCourseRepository = (IStudentCourseRepository)unitOfWork.Repository<StudentCourseRepository>();
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            this.configuration = configuration;
        }
        public PagingViewModel Get(string name = "", string orderBy = "ID", bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var list = _StudentRepository.GetAll();
            //if (!string.IsNullOrEmpty(name))
            //    list = list.Where(x => x.Name.Contains(name));
            int records = list.Count();
            if (records <= pageSize || pageIndex <= 0) pageIndex = 1;
            int pages = (int)Math.Ceiling((double)records / pageSize);
            int excludedRows = (pageIndex - 1) * pageSize;
            IEnumerable<StudentViewModel> result = new List<StudentViewModel>();
            var items = list.OrderByDescending(x=>x.ID).Skip(excludedRows).Take(pageSize);
            result = items.ToList().Select(x => Mapper.Map<StudentViewModel>(x)).ToList();
            return new PagingViewModel() { PageIndex = pageIndex, PageSize = pageSize, Result = result, Records = records, Pages = pages };
        }
        public  async Task<StudentEditViewModel> AddStudent(StudentEditViewModel model)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var identityUser = new IdentityUser
                    {
                        UserName = model.User.UserName,
                        PhoneNumber = model.User.PhoneNumber
                    };

                    var result = await _userManager.CreateAsync(identityUser, model.User.PasswordHash);
                    if (result.Succeeded)
                    {

                        var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                        var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                        var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                        string url = $"{configuration["AppUrl"]}/api/User/confirmemail?userid={identityUser.Id}&token={validEmailToken}";
                        await _userManager.AddToRoleAsync(identityUser, "Student");


                        var obj = Mapper.Map<Student>(model);
                        obj.User = null;
                        obj.ID = identityUser.Id;
                        obj.UserID = identityUser.Id;
                        var attachedObj = _repository.Add(obj);
                        _unitOfWork.Commit();

                        if (model.StudentCourses != null && model.StudentCourses.Count > 0)
                        {
                            foreach (var i in model.StudentCourses)
                                i.StudentID = attachedObj.ID;
                            _StudentCourseRepository.AddRange(model.StudentCourses.Select(i => Mapper.Map<StudentCourse>(i)));
                            _unitOfWork.Commit();
                        }

                        var StudentEditViewModel = Mapper.Map<StudentEditViewModel>(attachedObj);
                        transaction.Commit();

                        return StudentEditViewModel;
                    }
                    else
                    {
                       
                        throw new Exception("userName Already exist");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public async Task<StudentEditViewModel> EditStudent(StudentEditViewModel model)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(model.User.Id);
                    var UserByName = await _userManager.FindByNameAsync(model.User.UserName);
                    if(UserByName !=null)
                    {
                        if (model.User.UserName == UserByName.UserName && user.Id != UserByName.Id)
                        {
                            throw new Exception("username alewrdy exist");
                        }
                    }
                   
                    user.UserName = model.User.UserName;

                    user.PhoneNumber = model.User.PhoneNumber;
                    user.Id = model.ID;
                    await _userManager.UpdateAsync(user);
                    var obj = Mapper.Map<Student>(model);
                    var attachedObj = _repository.Edit(obj);

                    _unitOfWork.Commit();
                    if (model.StudentCourses != null && model.StudentCourses.Count > 0)
                    {
                        foreach (var item in model.StudentCourses)
                        {

                            item.StudentID = obj.ID;
                        }

                        var list = model.StudentCourses.Select(i => Mapper.Map<StudentCourse>(i)); ;
                        string[] ids = list.Select(i => i.ID).ToArray();
                        var Lessons = _StudentCourseRepository.Find(i => i.StudentID == obj.ID && !ids.Contains(i.ID));
                        _StudentCourseRepository.AddRange(list.Where(i => i.ID == null));
                        _StudentCourseRepository.EditRange(list.Where(i => i.ID != null));
                        _StudentCourseRepository.RemoveRange(Lessons.Select(i => i.ID).ToList());


                    }
                  //  var ids = _repository.GetById(model.ID).StudentCourses.Select(x => x.StudentID);
                    _unitOfWork.Commit();

                    transaction.Commit();
                    return Mapper.Map<StudentEditViewModel>(attachedObj);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<Object> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    // Get role assigned to the user
                    var role = await _userManager.GetRolesAsync(user);
                    IdentityOptions _options = new IdentityOptions();

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    var response = new
                    {
                        id = user.Id.ToString(),
                        auth_token = new { token },
                        role = role,
                        
                    };
                    var json = JsonConvert.SerializeObject(response);
                    return response;


                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public List<string> GetStudents(string CourseID)
        {
            var Students = _repository.Find(x => x.StudentCourses.Any(y=>y.CourseID== CourseID && !y.IsDeleted)).ToList();
            if (Students.Count > 0)
                return Students.Select(x => x.ID).ToList();
            return null;           
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCommon;
using Microsoft.AspNetCore.Mvc;
using UserCenter.API.Data;
using UserCenter.API.Exceptions;
using UserCenter.API.Services;
using UserCenter.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DotNetCore.CAP;
using UserCenter.API.Integration.Events;

namespace UserCenter.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        /// <summary>
        /// 数据库操作对象
        /// </summary>
        private readonly UserContext _Context;

        private readonly ICapPublisher _CapPublisher;
        /// <summary>
        /// 短信验证码验证服务
        /// </summary>
        private readonly ISmsCodeValidatorService _SmsCodeValidatorService;

        public UserController(UserContext Context, ICapPublisher CapPublisher, ISmsCodeValidatorService SmsCodeValidatorService)
        {
            _Context = Context;
            _SmsCodeValidatorService = SmsCodeValidatorService;
            _CapPublisher = CapPublisher;
        }

        [Route("GetStudent")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetStudent(string StudentID)
        {
            var student = await _Context.Students.FindAsync(StudentID);
            return Json(student);
        }

        /// <summary>
        /// 学生注册
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <returns></returns>
        [Route("Register_Student")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register_Student(RegisterStudentViewmodel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidatorException();
            }
            if (!_SmsCodeValidatorService.Validate(viewmodel.Mobile, viewmodel.Code))
            {
                throw new ValidatorException("验证码不正确");
            }
            var student = await _Context.Students.AddAsync(new Models.Student() {
                                                                        Mobile = viewmodel.Mobile,
                                                                        NickName = viewmodel.NickName,
                                                                        Password = MD5Builder.Builder32Hash(viewmodel.Password),
                                                                        Picture = viewmodel.Picture});
            int result =  await _Context.SaveChangesAsync();
            if (result == 0)
            {
                throw new ValidatorException("用户已存在，无法注册");
            }
            return Json(student.Entity);
        }

        /// <summary>
        /// 老师注册
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register_Teacher")]
        [AllowAnonymous]
        public async Task<IActionResult> Register_Teacher(RegisterTeacherViewmodel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidatorException();
            }
            if (!_SmsCodeValidatorService.Validate(viewmodel.Mobile, viewmodel.Code))
            {
                throw new ValidatorException("验证码不正确");
            }
            var teacher = await _Context.Teachers.AddAsync(new Models.Teacher {
                Introduce = viewmodel.Introduce,
                Mobile = viewmodel.Mobile,
                Name = viewmodel.Name,
                Password = MD5Builder.Builder32Hash(viewmodel.Password),
                Picture = viewmodel.Picture});
            int result = await _Context.SaveChangesAsync();
            if (result == 0)
            {
                throw new ValidatorException("该教师账号已存在");
            }
            return Json(teacher.Entity);
        }

        /// <summary>
        /// 学生登陆
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [Route("Student_LoginByPassword")]
        [HttpPost]
        [AllowAnonymous]
        public async Task< IActionResult> Student_LoginByPassword(LoginByPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidatorException();
            }
            var student = await _Context.Students.AsNoTracking().Where(vn => vn.Mobile == viewModel.Mobile).FirstOrDefaultAsync();
            if (student == null)
            {
                throw new ValidatorException("不存在该用户");
            }
            if (student.Password != MD5Builder.Builder32Hash(viewModel.Password))
            {
                throw new ValidatorException("登陆密码不正确");
            }
            return Json(student);
        }

        /// <summary>
        /// 老师登陆
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [Route("Teacher_LoginByPassword")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Teacher_LoginByPassword(LoginByPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidatorException();
            }
            var teacher = await _Context.Teachers.AsNoTracking().Where(vn => vn.Mobile == viewModel.Mobile).FirstOrDefaultAsync();
            if (teacher == null)
            {
                throw new ValidatorException("不存在该用户");
            }
            if (teacher.Password != MD5Builder.Builder32Hash(viewModel.Password))
            {
                throw new ValidatorException("登陆密码不正确");
            }
            return Json(teacher);
        }

        [Route("Student_LoginBySms")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Student_LoginBySms(LoginBySmsViewmodel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidatorException();
            }
            if (!_SmsCodeValidatorService.Validate(viewmodel.mobile, viewmodel.code))
            {
                throw new ValidatorException("无效的验证码");
            }
            var student = await _Context.Students.AsNoTracking().Where(vn => vn.Mobile == viewmodel.mobile).FirstOrDefaultAsync();
            if (student == null)
            {
                throw new ValidatorException("不存在该用户");
            }
            return Json(student);
        }

        [Route("Teacher_LoginBySms")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Teacher_LoginBySms(LoginBySmsViewmodel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidatorException();
            }
            if (!_SmsCodeValidatorService.Validate(viewmodel.mobile, viewmodel.code))
            {
                throw new ValidatorException("无效的验证码");
            }
            var teacher = await _Context.Teachers.AsNoTracking().Where(vn => vn.Mobile == viewmodel.mobile).FirstOrDefaultAsync();
            if (teacher == null)
            {
                throw new ValidatorException("不存在该用户");
            }
            return Json(teacher);
        }

        [Route("Student_Update")]
        [HttpPatch]
        public async Task<IActionResult> Student_Update([FromBody] StudentUpdateViewModel viewModel)
        {
            var student = await _Context.Students.FirstOrDefaultAsync(v => v.ID == UserID);
            if (student == null)
            {
                throw new ValidatorException("不存在该用户");
            }

            StudentUpdateIntegrationEvent @event = null;

            if (viewModel.Password != null)
            {
                student.Password = MD5Builder.Builder32Hash(viewModel.Password);
            }
            if (viewModel.Picture != null)
            {
                student.Picture = viewModel.Picture;
            }
            if (viewModel.NickName != null)
            {
                student.NickName = viewModel.NickName;
                @event = new StudentUpdateIntegrationEvent(UserID,viewModel.NickName);
            }

            using (var trans = _Context.Database.BeginTransaction(_CapPublisher,autoCommit:true))
            {
                if (@event != null)
                {
                    await _CapPublisher.PublishAsync("ELearn.UserCenter.StudentUpdate", @event);
                }
            }

            return Json(true);
        }
    }
}

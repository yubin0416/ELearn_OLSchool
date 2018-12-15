using IdentityServer.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 注册学生
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Task<Student> Register_Student(Student student);

        /// <summary>
        /// 注册老师
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        Task<Teacher> Register_Student(Teacher teacher);

        /// <summary>
        /// 学生账号密码登陆
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Student> Student_LoginByPassword(string mobile, string password);

        /// <summary>
        /// 老师账号密码登陆
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Teacher> Teacher_LoginByPassword(string mobile, string password);

        /// <summary>
        /// 学生通过短信登陆
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<Student> Student_LoginBySms(string mobile, string code);

        /// <summary>
        /// 老师通过短信登陆
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<Teacher> Teacher_LoginBySms(string mobile, string code);
    }
}

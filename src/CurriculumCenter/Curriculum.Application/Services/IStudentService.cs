using Curriculum.Application.Dtos;
using Curriculum.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curriculum.Application.Services
{
    public interface IStudentService
    {
        Task<StudentDto> GetStudent(string ID);
    }
}

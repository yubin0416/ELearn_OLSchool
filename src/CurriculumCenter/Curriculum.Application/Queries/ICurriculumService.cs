using Curriculum.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curriculum.Application.Queries
{
    public interface ICurriculumService
    {
        /// <summary>
        /// 通过课程ID获取课程对象
        /// </summary>
        /// <param name="CurriculumID"></param>
        /// <returns></returns>
        Task<CurriculumViewModel> GetCurrcilumByIDAsync(string CurriculumID);

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DDD.Framework;
using System.Threading.Tasks;

namespace Curriculum.Domain
{
    public interface ICurriculumRepository:IResposity<Curriculum>
    {
        Task<Curriculum> GetCurriculumByIDAsync(string CurriculumID);

        Task<Curriculum> AddCurriculumAsync(Curriculum curriculum);

        Task UpdateCurriculumAsync(Curriculum curriculum);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Curriculum.Domain;
using DDD.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Curriculum.Infrastruction
{
    public class CurriculumRepository : ICurriculumRepository
    {
        private readonly CurriculumContext context;

        public CurriculumRepository(CurriculumContext _context)
        {
            context = _context;
        }

        public IUnitwork _Unitwork => context;

        public async Task<Domain.Curriculum> AddCurriculumAsync(Domain.Curriculum curriculum)
        {
            return (await context.Curriculums.AddAsync(curriculum)).Entity;
        }

        public async Task<Domain.Curriculum> GetCurriculumByIDAsync(string CurriculumID)
        {
            var curriculum = await context.Curriculums
                                                .Include(d=>d.Comments)
                                                .Include(d=>d.Coursewares)
                                                .Include(d=>d.LearningStudents)
                                                .Include(d => d.sections)
                                                .ThenInclude(d => d.ClassHours)
                                                .Where(vn=>vn.ID == CurriculumID)
                                                .FirstOrDefaultAsync();
            return curriculum;
        }

        public Task UpdateCurriculumAsync(Domain.Curriculum curriculum)
        {
            context.Attach(curriculum);
            context.Entry(curriculum).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}

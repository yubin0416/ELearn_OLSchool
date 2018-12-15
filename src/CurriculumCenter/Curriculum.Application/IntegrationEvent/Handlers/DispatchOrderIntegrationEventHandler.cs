using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Curriculum.Application.Services;
using Curriculum.Domain;
using Curriculum.Domain.Exception;
using DotNetCore.CAP;

namespace Curriculum.Application.IntegrationEvent.Handlers
{
    public class DispatchOrderIntegrationEventHandler : IIntegrationHandler<DispatchOrderIntegrationEvent>, ICapSubscribe
    {
        private readonly ICurriculumRepository _CurriculumRepository;
        private readonly IStudentService _StudentService;

        public DispatchOrderIntegrationEventHandler(ICurriculumRepository CurriculumRepository, IStudentService StudentService)
        {
            _CurriculumRepository = CurriculumRepository;
            _StudentService = StudentService;
        }

        [CapSubscribe("ELearn.OrderCenter.DispatchOrder")]
        public async Task Handler(DispatchOrderIntegrationEvent Event)
        {
            var Curriculum = await _CurriculumRepository.GetCurriculumByIDAsync(Event.CurriculumID);
            if (Curriculum == null) throw new CurriculumExecption("课程不存在");
            var studentdto = await _StudentService.GetStudent(Event.StudentID);
            if (studentdto == null) throw new CurriculumExecption("学生不存在");
            Curriculum.AddStudent(new Student(studentdto.ID, studentdto.NickName, studentdto.Picture));
            await _CurriculumRepository._Unitwork.DomianSaveChangesAnsyc();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Curriculum.Domain;
using MediatR;

namespace Curriculum.Application.Commands.Handlers
{
    public class StudentLearnCurriculumCommandHandler : IRequestHandler<StudentLearnCurriculumCommand, bool>
    {
        private readonly ICurriculumRepository _CurriculumRepository;

        public StudentLearnCurriculumCommandHandler(ICurriculumRepository CurriculumRepository)
        {
            _CurriculumRepository = CurriculumRepository;
        }

        public async Task<bool> Handle(StudentLearnCurriculumCommand request, CancellationToken cancellationToken)
        {
            var CurriculumModel = await _CurriculumRepository.GetCurriculumByIDAsync(request.CurriculumID);
            CurriculumModel.AddStudent(new Student (request.StudentID,request.StudentNickName,request.StudentPicture ));
            return await _CurriculumRepository._Unitwork.DomianSaveChangesAnsyc();
        }
    }
}

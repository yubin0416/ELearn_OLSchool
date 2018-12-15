using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Curriculum.Domain;
using Curriculum.Domain.Exception;
using MediatR;

namespace Curriculum.Application.Commands.Handlers
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, bool>
    {
        private readonly ICurriculumRepository _CurriculumRepository;

        public CreateSectionCommandHandler(ICurriculumRepository CurriculumRepository)
        {
            _CurriculumRepository = CurriculumRepository;
        }

        public async Task<bool> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var CurriculumModel = await _CurriculumRepository.GetCurriculumByIDAsync(request.CurriculumID);
            if (CurriculumModel == null || CurriculumModel.Lecturer.TeacherID != request.TeacherID)
            {
                throw new CurriculumExecption("This Curriculum does not belong to you!");
            }
            CurriculumModel.AddSection(request.SectionTitle,request.SectionIntroduce);
            return await _CurriculumRepository._Unitwork.DomianSaveChangesAnsyc();
        }
    }
}

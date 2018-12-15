using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Curriculum.Domain;
using MediatR;

namespace Curriculum.Application.Commands.Handlers
{
    public class CreateCoursewareCommandHandler : IRequestHandler<CreateCoursewareCommand, bool>
    {
        private readonly ICurriculumRepository _CurriculumRepository;

        public CreateCoursewareCommandHandler(ICurriculumRepository CurriculumRepository)
        {
            _CurriculumRepository = CurriculumRepository;
        }

        public async Task<bool> Handle(CreateCoursewareCommand request, CancellationToken cancellationToken)
        {
            var CurriculumModel = await _CurriculumRepository.GetCurriculumByIDAsync(request.CurriculumID);
            CurriculumModel.AddCourseware(new Courseware() { Title = request.CoursewareTitle, DownloadUrl = request.DownloadUrl});
            return await _CurriculumRepository._Unitwork.DomianSaveChangesAnsyc();
        }
    }
}

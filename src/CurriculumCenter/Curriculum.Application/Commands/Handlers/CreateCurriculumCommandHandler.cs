using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Curriculum.Domain;
using MediatR;
using CurriculumModel = Curriculum.Domain.Curriculum;

namespace Curriculum.Application.Commands.Handlers
{
    public class CreateCurriculumCommandHandler : IRequestHandler<CreateCurriculumCommand,Domain.Curriculum>
    {
        private readonly ICurriculumRepository _CurriculumRepository;

        public CreateCurriculumCommandHandler(ICurriculumRepository CurriculumRepository)
        {
            _CurriculumRepository = CurriculumRepository;
        }

        public async Task<CurriculumModel> Handle(CreateCurriculumCommand request, CancellationToken cancellationToken)
        {
            CurriculumModel model = new CurriculumModel(
                new Teacher { Introduce = request.TeacherIntroduce,
                                        Name = request.TeacherName,
                                        Picture = request.TeacherPicture,
                                        TeacherID = request.TeacherID},
                request.Title,
                request.Introduce,
                request.PictureURL,
                request.IsFree,
                request.EndTime??DateTime.Parse("2999-1-1"));
            var result = await _CurriculumRepository.AddCurriculumAsync(model);
            await _CurriculumRepository._Unitwork.DomianSaveChangesAnsyc();
            return result;
        }
    }
}

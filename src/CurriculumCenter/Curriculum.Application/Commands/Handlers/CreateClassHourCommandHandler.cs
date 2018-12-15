using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Curriculum.Domain;
using Curriculum.Domain.Exception;
using System.Linq;
using AutoMapper;

namespace Curriculum.Application.Commands.Handlers
{
    public class CreateClassHourCommandHandler : IRequestHandler<CreateClassHourCommand, bool>
    {
        private readonly ICurriculumRepository _CurriculumRepository;
        private readonly IMapper _Mapper;

        public CreateClassHourCommandHandler(ICurriculumRepository CurriculumRepository,  IMapper Mapper)
        {
            _CurriculumRepository = CurriculumRepository;
            _Mapper = Mapper;
        }

        public async Task<bool> Handle(CreateClassHourCommand request, CancellationToken cancellationToken)
        {
            var CurriculumModel = await _CurriculumRepository.GetCurriculumByIDAsync(request.CurriculumID);
            if (CurriculumModel == null && CurriculumModel.Lecturer.TeacherID != request.TeacherID)
            {
                throw new CurriculumExecption("This Curriculum does not belong to you and you cannot edit it");
            }
            //var ClassHourModel = _Mapper.Map<ClassHour>(request);
            var ClassHourModel = new ClassHour() {
                            ClassHourTitle = request.ClassHourTitle,
                            ClassHourType = request.ClassHourType,
                            IsExperience = request.IsExperience,
                            IsFree = request.IsFree,
                            VedioDuration = request.VedioDuration,
                            VedioUrl = request.VedioUrl,
                            SectionID = request.SectionID
            };
            CurriculumModel.AddClassHour(request.SectionID, ClassHourModel);

            return await _CurriculumRepository._Unitwork.DomianSaveChangesAnsyc();
        }
    }
}

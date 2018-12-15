using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Curriculum.Domain;
using MediatR;

namespace Curriculum.Application.Commands.Handlers
{
    public class StudentCommentCommandHandler : IRequestHandler<StudentCommentCommand, bool>
    {
        private readonly ICurriculumRepository _CurriculumRepository;

        public StudentCommentCommandHandler(ICurriculumRepository CurriculumRepository)
        {
            _CurriculumRepository = CurriculumRepository;
        }

        public async Task<bool> Handle(StudentCommentCommand request, CancellationToken cancellationToken)
        {
            var CurriculumModel = await _CurriculumRepository.GetCurriculumByIDAsync(request.CurriculumID);

            CurriculumModel.AddComment(
                new Comment {
                    CommentStar = request.CommentStar,
                    Context = request.Context,
                    CurriculumID = request.CurriculumID,
                    StudentID = request.StudentID
            });

            return await _CurriculumRepository._Unitwork.DomianSaveChangesAnsyc();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Curriculum.Application.Commands;
using Curriculum.Application.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Curriculum.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class CurriculumController : BaseController
    {
        private readonly IMediator Mediator;
        private readonly ICurriculumService _QuriesService;

        public CurriculumController(IMediator _Mediator)
        {
            Mediator = _Mediator;
        }

        [HttpPost]
        [Authorize(Roles ="teacher")]
        public IActionResult AddCurriculum(CreateCurriculumCommand command)
        {
            command.TeacherID = UserID;
            var response = Mediator.Send(command);
            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "teacher")]
        public IActionResult AddSection(CreateSectionCommand command)
        {
            command.TeacherID = UserID;
            var response = Mediator.Send(command);
            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "teacher")]
        public IActionResult AddClassHour(CreateClassHourCommand command)
        {
            command.TeacherID = UserID;
            var response = Mediator.Send(command);
            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "student")]
        public IActionResult AddComment(StudentCommentCommand command)
        {
            command.StudentID = UserID;
            var response = Mediator.Send(command);
            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "teacher")]
        public IActionResult AddCourseware(CreateCoursewareCommand command)
        {
            var response = Mediator.Send(command);
            return Json(response);
        }

    }
}

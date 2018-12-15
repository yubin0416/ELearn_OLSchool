using System;
using System.Collections.Generic;
using System.Text;
using Curriculum.Application.Commands;
using FluentValidation;

namespace Curriculum.Application.Validators
{
    public class CreateClassHourCommandValidator : AbstractValidator<CreateClassHourCommand>
    {
        public CreateClassHourCommandValidator()
        {
            RuleFor(v => v.ClassHourTitle).MaximumLength(50).WithMessage("课时标题必须小于50个字！").NotEmpty().WithMessage("课时标题不能为空");
            RuleFor(v => v.ClassHourType).NotEmpty().WithMessage("");
        }
    }
}

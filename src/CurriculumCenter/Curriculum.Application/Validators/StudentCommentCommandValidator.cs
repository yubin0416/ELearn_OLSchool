﻿using System;
using System.Collections.Generic;
using System.Text;
using Curriculum.Application.Commands;
using FluentValidation;

namespace Curriculum.Application.Validators
{
    public class StudentCommentCommandValidator:AbstractValidator<StudentCommentCommand>
    {
        public StudentCommentCommandValidator()
        {

        }
    }
}

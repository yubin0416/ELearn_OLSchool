using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using OrderCenter.Application.Command;

namespace OrderCenter.Application.Validator
{
    public class CreateOrderCommandValidator:AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {

        }
    }
}

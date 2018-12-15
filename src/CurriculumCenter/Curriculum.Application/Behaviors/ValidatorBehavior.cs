using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using System.Linq;
using Curriculum.Domain.Exception;

namespace Curriculum.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>[] validators;

        public ValidatorBehavior(IValidator<TRequest>[] Validators)
        {
            validators = Validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = validators.Select(v => v.Validate(request))
                                                   .SelectMany(v => v.Errors)
                                                   .Where(v => v.ErrorMessage != null)
                                                   .ToList();

            if (failures.Any())
            {
                throw new CurriculumExecption($"{typeof(TRequest)} validate is erro");
            }
            return await next();
        }
    }
}

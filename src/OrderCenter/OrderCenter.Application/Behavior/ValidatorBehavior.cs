using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using System.Linq;
using OrderCenter.Domain.Exceptions;

namespace OrderCenter.Application.Behavior
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>[] validators;

        public ValidatorBehavior(IValidator<TRequest>[] _validators)
        {
            validators = _validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
             var failure = validators.Select(v => v.Validate(request))
                            .SelectMany(v => v.Errors)
                            .Where(v => v.ErrorMessage != null)
                            .ToList();
            if (failure.Any())
            {
                throw new OrderDomainException("验证有误");
            }
            return await next();
        }
    }
}

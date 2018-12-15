using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Integration.Handlers
{
    public interface IIntegrationEventHandler<T>
    {
        Task Handler(T model);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curriculum.Application.IntegrationEvent
{
    public interface IIntegrationHandler<T>
    {
        Task Handler(T Event);
    }
}

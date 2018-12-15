using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Application.Integration
{
    public interface IIntegrationHandler<T>
    {
        Task Handler(T Event);
    }
}

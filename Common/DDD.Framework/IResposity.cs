using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Framework
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public interface IResposity<TAggregateRoot> where TAggregateRoot:IAggregateRoot
    {
        IUnitwork _Unitwork { get; }
    }
}

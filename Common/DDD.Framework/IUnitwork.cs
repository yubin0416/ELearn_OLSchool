using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DDD.Framework
{
    public interface IUnitwork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> DomianSaveChangesAnsyc(CancellationToken cancellationToken = default(CancellationToken));
    }
}

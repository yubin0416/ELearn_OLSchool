using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;

namespace Curriculum.Application.IntegrationEvent.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class StudentUpdateIntegrationEventHandler : IIntegrationHandler<StudentUpdateIntegrationEvent>, ICapSubscribe
    {
        [CapSubscribe("ELearn.UserCenter.StudentUpdate")]
        public Task Handler(StudentUpdateIntegrationEvent Event)
        {
            return Task.CompletedTask;
        }
    }
}

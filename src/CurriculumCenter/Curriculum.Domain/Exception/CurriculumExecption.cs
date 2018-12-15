using System;
using System.Collections.Generic;
using System.Text;

namespace Curriculum.Domain.Exception
{
    public class CurriculumExecption:System.Exception
    {
        public CurriculumExecption() : base() { }

        public CurriculumExecption(string message) : base(message) { }

        public CurriculumExecption(string message, System.Exception exception) : base(message,exception) { }
    }
}

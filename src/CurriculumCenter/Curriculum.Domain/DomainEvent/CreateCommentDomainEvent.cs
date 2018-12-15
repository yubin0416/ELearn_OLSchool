using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Curriculum.Domain.DomainEvent
{
    public class CreateCommentDomainEvent:INotification
    {
        public Comment GetComment { get; }

        public CreateCommentDomainEvent(Comment _Comment)
        {
            GetComment = _Comment;
        }
    }
}

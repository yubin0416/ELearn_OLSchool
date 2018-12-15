using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace DDD.Framework
{
    public abstract class Entity
    {
        public int ID { get; protected set; }

        protected  List<INotification> _DomainEventList;
        public List<INotification> DomainEventList { get { return _DomainEventList; } }

        protected Entity()
        {
            _DomainEventList = new List<INotification>();
        }

        public virtual void AddDomianEvent(INotification domainevent)
        {
            _DomainEventList.Add(domainevent);
        }

        public virtual void Clear()
        {
            _DomainEventList.Clear();
        }

        public virtual bool IsTemporary()
        {
            if (ID.Equals(default(int)))
            {
                return true;
            }
            return false;
        }
    }

    public abstract class Entity<TKey> : Entity where TKey :IComparable
    {
        public new TKey ID { get; protected set; }

        public Entity():base()
        {

        }

        public override bool IsTemporary()
        {
            if (ID == null || ID.Equals(default(TKey)))
            {
                return true;
            }
            return false;
        }
    }
}

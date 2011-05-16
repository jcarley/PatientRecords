using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;

namespace EventHandlers
{
    public interface IHandlesEvent<T>
        where T : DomainEvent
    {
        void Handle(T domainEvent);
    }
}

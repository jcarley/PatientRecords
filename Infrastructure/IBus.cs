using System;
using Commands;
using Events;

namespace Infrastructure
{
    public interface IBus
    {
        void Send<T>(T command)
            where T : Command;

        void RegisterHandler<T>(Action<T> handler)
            where T : DomainEvent;
    }
}

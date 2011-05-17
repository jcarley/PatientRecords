using System;

namespace Commands
{
    public abstract class Command
    {
        public readonly Guid Id;
        public readonly Guid ClientId;

        protected Command(Guid id, Guid clientId)
        {
            Id = id;
            ClientId = clientId;
        }
    }
}

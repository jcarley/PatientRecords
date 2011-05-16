using System;
using System.Collections.Generic;
using CommandHandlers;
using Commands;
using Events;
using EventStore;
using EventStore.Dispatcher;
using StructureMap;

namespace Infrastructure.Bus
{
    
    public class InProcessBus : IBus, IPublishMessages
    {
        private readonly Dictionary<Type, List<Action<DomainEvent>>> _routes = new Dictionary<Type, List<Action<DomainEvent>>>();

        public InProcessBus()
        {
        }

        /// <summary>
        /// Queues a command onto the service bus
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        public void Send<T>(T command)
            where T : Command
        {
            // the service bus version would queue the command

            var handler = GetCommandHandlerForCommand<T>();
            if (handler != null)
                handler.Handle(command);
        }

        /// <summary>
        /// Register a handler for a type of DomainEvent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        public void RegisterHandler<T>(Action<T> handler)
            where T : DomainEvent
        {
            List<Action<DomainEvent>> handlers;
            if (!_routes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<DomainEvent>>();
                _routes.Add(typeof(T), handlers);
            }
            handlers.Add(DelegateAdjuster.CastArgument<DomainEvent, T>(x => handler(x)));
        }

        /// <summary>
        /// Publishes an event
        /// </summary>
        /// <param name="commit"></param>
        public void Publish(Commit commit)
        {
            // the service bus version would publish the event to the queue

            foreach (var @event in commit.Events)
            {
                List<Action<DomainEvent>> handlers;

                if (!_routes.TryGetValue(@event.Body.GetType(), out handlers)) return;

                foreach (var handler in handlers)
                {
                    handler((DomainEvent)@event.Body);
                }
            }
        }

        /// <summary>
        /// Resolves the handler that supports the command type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private IHandles<T> GetCommandHandlerForCommand<T>()
                where T : Command
        {
            return ObjectFactory.GetInstance<IHandles<T>>();
        }

        public void Dispose()
        {
        }
    }
}

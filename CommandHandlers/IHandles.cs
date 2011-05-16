using Commands;

namespace CommandHandlers
{
    public interface IHandles<in T> : IHandles
        where T : Command
    {
        void Handle(T command);
    }

    public interface IHandles
    {
    }
}

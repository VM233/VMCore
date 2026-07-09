namespace VMFramework.Core
{
    public interface IPriorityOwner<out TPriority>
    {
        public TPriority Priority { get; }
    }
}
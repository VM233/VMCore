namespace VMFramework.Core.Pools
{
    public interface IPoolEventProvider : IReturnEventProvider
    {
        public delegate void GetHandler(IPoolEventProvider provider);
        
        public event GetHandler OnGetEvent;
    }
}
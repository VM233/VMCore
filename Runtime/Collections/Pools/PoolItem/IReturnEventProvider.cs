namespace VMFramework.Core.Pools
{
    public interface IReturnEventProvider
    {
        public delegate void ReturnHandler(IReturnEventProvider provider);
        
        public event ReturnHandler OnReturnEvent;
    }
}
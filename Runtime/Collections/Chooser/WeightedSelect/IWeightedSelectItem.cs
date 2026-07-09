namespace VMFramework.Core
{
    public interface IWeightedSelectItem
    {
        float Weight { get; }
    }
    
    public interface IWeightedSelectItem<out T> : IWeightedSelectItem
    {
        public T Value { get; }
    }
}
namespace VMFramework.Core
{
    public interface IChooser : IRandomItemProvider
    {
        public IChooser GenerateNewObjectChooser();
    }

    public interface IChooser<out T> : IChooser, IRandomItemProvider<T>
    {
        public IChooser<T> GenerateNewChooser();

        IChooser IChooser.GenerateNewObjectChooser()
        {
            return GenerateNewChooser();
        }
    }
}
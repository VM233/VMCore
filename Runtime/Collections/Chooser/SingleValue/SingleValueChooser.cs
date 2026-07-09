using System;
using System.Runtime.CompilerServices;
using VMFramework.Core.Generic;

namespace VMFramework.Core
{
    [Serializable]
    public class SingleValueChooser<TItem> : IChooser<TItem>, IMinimumClampable<double>, IMaximumClampable<double>
    {
        public TItem value;

        public SingleValueChooser()
        {
            
        }

        public SingleValueChooser(TItem value)
        {
            this.value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TItem GetRandomItem(Random random)
        {
            return value;
        }

        object IRandomItemProvider.GetRandomObjectItem(Random random)
        {
            return value;
        }

        public IChooser<TItem> GenerateNewChooser()
        {
            return new SingleValueChooser<TItem>(value);
        }

        public IChooser GenerateNewObjectChooser()
        {
            return new SingleValueChooser<TItem>(value);
        }

        public bool TryClampByMinimum(double minimum)
        {
            if (typeof(TItem).IsNumber())
            {
                value = value.ClampMin(minimum);
                return true;
            }

            return false;
        }
        
        public bool TryClampByMaximum(double maximum)
        {
            if (typeof(TItem).IsNumber())
            {
                value = value.ClampMax(maximum);
                return true;
            }

            return false;
        }
    }
}
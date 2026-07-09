using System;
using System.Collections.Generic;

namespace VMFramework.Core
{
    public sealed class NestedWeightedSelectChooser<TItem> : WeightedSelectChooserBase<IChooser<TItem>>, IChooser<TItem>
    {
        public NestedWeightedSelectChooser(params (IChooser<TItem> item, float weight)[] infos) : base(infos)
        {
        }

        public NestedWeightedSelectChooser(IChooser<TItem>[] values, float[] weights) : base(values, weights)
        {
        }

        public NestedWeightedSelectChooser(IReadOnlyList<IChooser<TItem>> values) : base(values)
        {
        }

        public NestedWeightedSelectChooser(IReadOnlyList<WeightedSelectItem<IChooser<TItem>>> items) : base(items)
        {
        }

        public NestedWeightedSelectChooser(IReadOnlyList<IWeightedSelectItem<IChooser<TItem>>> items) : base(items)
        {
        }

        public NestedWeightedSelectChooser(IReadOnlyDictionary<IChooser<TItem>, float> itemDict) : base(itemDict)
        {
        }

        public TItem GetRandomItem(Random random)
        {
            var chooser = random.WeightedChoose(infos);
            if (chooser == null)
            {
                return default;
            }

            return chooser.GetRandomItem(random);
        }

        public IChooser<TItem> GenerateNewChooser()
        {
            var newInfos = new (IChooser<TItem>, float)[infos.Length];
            for (int i = 0; i < infos.Length; i++)
            {
                var (chooser, weight) = infos[i];
                newInfos[i] = (chooser.GenerateNewChooser(), weight);
            }

            return new NestedWeightedSelectChooser<TItem>(newInfos);
        }
    }
}
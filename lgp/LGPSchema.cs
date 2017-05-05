using System;

namespace lgp
{
    public class LGPSchema
    {
        public enum CrossoverType
        {
            linear,
            one_point,
            one_seg
        }

        public CrossoverType Crossover = CrossoverType.linear;
    }
}

using System.Collections.Generic;
using System.Linq;

namespace WeightCalculator
{
    internal class ArrayComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] x, int[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (y.FirstOrDefault(w => { return w == x[i]; }) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(int[] obj)
        {
            return obj.GetHashCode();
        }
    }
}

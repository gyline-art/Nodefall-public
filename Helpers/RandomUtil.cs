using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Helpers
{
    public static class RandomUtil
    {
        public static T PickOne<T>(IReadOnlyList<T> list)
        {
            if (list.Count == 0)
                throw new InvalidOperationException("List cannot be empty");

            int i = Random.Shared.Next(list.Count);
            return list[i];
        }
    }
}

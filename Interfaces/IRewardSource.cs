using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Interfaces
{
    public interface IRewardSource
    {
        int Gold { get; }
        int Exp { get; }
    }
}

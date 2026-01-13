using Nodefall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Combat.Advantage
{
    public class Advantage
    {
        public IAdvantage Source { get; }
        public IAdvantage Target { get; }

        public Advantage(IAdvantage source, IAdvantage target)
        {
            Source = source;
            Target = target;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Event
{
    public abstract class EventCard
    {
        public abstract string Title { get; }
        public abstract string Description { get; }

        public abstract void Apply(GameManager game);
    }
}

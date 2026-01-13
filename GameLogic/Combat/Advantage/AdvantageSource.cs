using Nodefall.Interfaces;
using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Combat.Advantage
{
    public abstract class AdvantageSource : IAdvantage
    {
        public abstract List<Advantage> Advantages { get; }
        public virtual void ApplyTo(Character c)
        {
            foreach (var advantage in Advantages)
            {
                c.AdvantageSources.Add(advantage.Source);
                c.AdvantageTargets.Add(advantage.Target);
            }
        }
        public virtual void RemoveFrom(Character c)
        {
            foreach (var advantage in Advantages)
            {
                c.AdvantageSources.Remove(advantage.Source);
                c.AdvantageTargets.Remove(advantage.Target);
            }
        }
    }
}

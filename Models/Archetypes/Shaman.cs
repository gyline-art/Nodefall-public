using Nodefall.GameLogic.Combat.Advantage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models.Archetypes
{
    public sealed class Shaman : Archetype
    {
        public static readonly Shaman Instance = new();
        public override string Name => "Shaman";
        public override int BonusHP => 0;
        public override int BonusATK => 2;
        public override int BonusMP => 6;
        public override List<Advantage> Advantages => new() { new Advantage(this, Warrior.Instance) };
    }
}

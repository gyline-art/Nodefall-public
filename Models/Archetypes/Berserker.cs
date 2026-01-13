using Nodefall.GameLogic.Combat.Advantage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models.Archetypes
{
    public sealed class Berserker : Archetype
    {
        public static readonly Berserker Instance = new();
        public override string Name => "Berserker";
        public override int BonusHP => 5;
        public override int BonusATK => 7;
        public override int BonusMP => -1;
        public override List<Advantage> Advantages => new();
    }
}

using Nodefall.GameLogic.Combat.Advantage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models.Archetypes
{
    public sealed class Assassin : Archetype
    {
        public static readonly Assassin Instance = new();
        public override string Name => "Assassin";
        public override int BonusHP => 3;
        public override int BonusATK => 5;
        public override int BonusMP => 2;
        public override List<Advantage> Advantages => new() { new Advantage(this, Shaman.Instance) };
    }
}

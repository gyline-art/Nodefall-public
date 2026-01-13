using Nodefall.GameLogic.Combat.Advantage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models.Archetypes
{
    public sealed class Warrior : Archetype
    {
        public static readonly Warrior Instance = new();
        public override string Name => "Warrior";
        public override int BonusHP => 5;
        public override int BonusATK => 4;
        public override int BonusMP => 0;
        public override List<Advantage> Advantages => new(){new Advantage(this, Assassin.Instance)};
    }
}

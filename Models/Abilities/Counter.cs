using Nodefall.GameLogic.Combat.Advantage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models.Abilities
{
    public class Counter : Ability
    {
        public override string Name => "Counter";
        public override string Description => "After being hit, immediately launch an attack";
        public override AbilityTargetType Type => AbilityTargetType.AllEnemies;
        public override void Use(Character user, IEnumerable<Character> targets)
        {
            user.IsDefending = true;
            while (user.IsDefending)
            {
                if (user.Hit())
                    foreach (var target in targets)
                    {
                        target.Hit(AdvantageSystem.CalculateDmg(10, user, target));
                    }
                user.IsDefending = false;
            }
        }
        public override Ability CreateCopy() => new Counter();
    }
}

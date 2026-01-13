using Nodefall.GameLogic.Combat.Advantage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models.Abilities
{
    public class Fireball : Ability
    {
        public override string Name => "Fireball";
        public override string Description => "Rolls a D20 for your Attack";
        public override AbilityTargetType Type => AbilityTargetType.SingleEnemy;
        public override void Use(Character user, IEnumerable<Character> targets)
        {
            var d20 = new Random().Next(20);
            // Crit Fail = 0 by design

            foreach (var target in targets)
            {
                target.Hit(AdvantageSystem.CalculateDmg(d20, user, target));
            }
            user.IsDefending = false;
            user.CurrentMP--;
        }
        public override Ability CreateCopy() => new Fireball();
    }
}

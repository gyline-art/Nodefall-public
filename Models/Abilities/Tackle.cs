using Nodefall.GameLogic.Combat.Advantage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models.Abilities
{
    public class Tackle : Ability
    {
        public override string Name => "Tackle";
        public override string Description => "Deals minor DMG to all enemies while blocking";
        public override AbilityTargetType Type => AbilityTargetType.AllEnemies;
        public override void Use(Character user, IEnumerable<Character> targets)
        {
            int damage = 4;
            user.IsDefending = true;
            foreach (var target in targets)
            {
                target.Hit(AdvantageSystem.CalculateDmg(damage, user, target));
            }
        }
        public override Ability CreateCopy() => new Tackle();
    }
}

using Nodefall.GameLogic.Combat.Advantage;
using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Combat
{
    public static class BattleEngine
    {
        public static void Attack(Character user, IEnumerable<Character> targets)
        {
            int damage = user.ATK;
            foreach (var target in targets.Where(t => t.IsAlive))
            {
                target.Hit(AdvantageSystem.CalculateDmg(damage, user, target));
            }
            user.IsDefending = false;
        }

        public static void Defend(Character user)
        {
            user.IsDefending = true;
        }

        public static void UseAbility(Character user, IEnumerable<Character> targets)
        {
            user.Ability.Use(user, targets);
        }
    }
}

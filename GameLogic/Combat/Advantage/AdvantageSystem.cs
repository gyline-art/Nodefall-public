using Nodefall.Models;
using Nodefall.Models.Archetypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Combat.Advantage
{
    public static class AdvantageSystem
    {
        public const float AdvantageMultiplier    = 1.25f;
        public const float DisadvantageMultiplier = 0.75f;

        public static int CalculateDmg(int baseDmg, Character user, Character target)
        {
            float advantage    = GetMultiplier(user, target, AdvantageMultiplier);
            float disadvantage = GetMultiplier(target, user, DisadvantageMultiplier);

            return (int)Math.Ceiling(baseDmg * advantage * disadvantage);
        }

        private static float GetMultiplier(Character user, Character target, float multiplier)
        {
            var result = 1f;

            if (user.AdvantageTargets.Count == 0) return result;
            if (multiplier > 1 && (user.Archetype is Berserker || target.Archetype is Berserker))
            {
                result *= multiplier;
            }

            foreach (var userAdvantage in user.AdvantageTargets)
            {
                if (target.AdvantageSources.Contains(userAdvantage))
                    result *= multiplier;
            }

            return result;
        }
    }
}

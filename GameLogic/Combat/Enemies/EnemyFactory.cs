using Nodefall.Helpers;
using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Combat.Enemies
{
    public static class EnemyFactory
    {
        public static List<Enemy> CreateEncounter(Encounter e, int difficulty)
        {
            int maxExtra = e.MaxEnemies - e.MinEnemies;
            int scaledExtra = Math.Min(difficulty - 1, maxExtra);
            int count = Random.Shared.Next(e.MinEnemies + scaledExtra, e.MaxEnemies + 1);

            bool usePreset = e.Presets.Count > 0 && Random.Shared.NextDouble() < 0.33;

            var enemies = new List<Enemy>();

            for (int i = 0; i < count; i++)
            {
                Enemy enemy;

                if (usePreset || e.UseOnlyPresets)
                {
                    var preset = RandomUtil.PickOne(e.Presets);
                    enemy = new Enemy
                        (preset.EnemyType,
                         preset.Archetype,
                         preset.Ability,
                         preset.Name,
                         difficulty: difficulty);
                }
                else
                {
                    enemy = new Enemy
                        (RandomUtil.PickOne(e.EnemyTypes),
                         RandomUtil.PickOne(e.Archetypes),
                         RandomUtil.PickOne(e.Abilities),
                         difficulty: difficulty);
                }

                enemies.Add(enemy);
            }

            return enemies;
        }
    }
}

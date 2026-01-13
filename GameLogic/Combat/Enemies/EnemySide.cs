using Nodefall.GameLogic.Combat.Targeting;
using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Nodefall.GameLogic.Combat.Enemies
{
    public class EnemySide
    {
        public List<Enemy> AllEnemies { get; }
        public List<Enemy> ActiveEnemies { get; }

        private const int MaxActive = 5;

        public EnemySide(List<Enemy> enemies) 
        {
            AllEnemies = enemies;
            ActiveEnemies = enemies.Take(MaxActive).ToList();
        }

        public bool HasLivingUnits => ActiveEnemies.Any(e => e.IsAlive);

        public async Task TakeTurn(BattleManager battle)
        {
            var livingUnits = ActiveEnemies.Where(e => e.IsAlive).ToList();
            foreach (var enemy in livingUnits)
            {
                enemy.IsTakingAction = true;
                await Task.Delay(900);

                var (action,target) = EnemyAI.ChooseAction(enemy, battle);
                var targets = TargetingSystem.ResolveTargets(enemy, action, battle, target);

                battle.ResolveAction(enemy, action, targets);
                await Task.Delay(900);

                if (!battle.Player.IsAlive) return;
                enemy.IsTakingAction = false;
            }
        }

        public void RefreshActiveUnits()
        {
            ActiveEnemies.RemoveAll(e => !e.IsAlive);

            while(ActiveEnemies.Count < MaxActive)
            {
                var nextEnemy = AllEnemies.FirstOrDefault(e => e.IsAlive && !ActiveEnemies.Contains(e));

                if (nextEnemy == null) break;
                ActiveEnemies.Add(nextEnemy);
            }
        }

        public (int gold, int exp) GetFinalRewards()
        {
            int gold = 0;
            int exp = 0;
            var defeatedEnemies = AllEnemies.Where(e => !e.IsAlive).ToList();
            foreach (var enemy in defeatedEnemies)
            {
                gold += enemy.Type.Gold;
                exp += enemy.Type.Exp;
            }

            return (gold, exp);
        }
    }
}

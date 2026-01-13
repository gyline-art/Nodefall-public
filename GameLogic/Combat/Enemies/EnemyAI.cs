using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Combat.Enemies
{
    public static class EnemyAI
    {
        public static (BattleAction action, Character? target) ChooseAction(Enemy self, BattleManager battle)
        {
            if (IsVeryLowHP(self))
            {
                if (CanHeal(self) && CanUseAbility(self))
                    return (BattleAction.UseAbility, self);
                return (BattleAction.Defend, null);
            }

            if (CanHeal(self) && CanUseAbility(self))
            {
                var target = ChooseHealingTarget(battle);
                if (target != null) 
                    return (BattleAction.UseAbility, target);
            }

            if (IsVeryLowHP(battle.Player))
            {
                if (CanUseAbility(self) && AbilityIsAttackType(self))
                    return (BattleAction.UseAbility, battle.Player);
                return (BattleAction.Attack, battle.Player);
            }

            if (CanBuff(self) && CanUseAbility(self))
            {
                return (BattleAction.UseAbility, null);
            }

            return WeightedChoice(self, battle);
        }

        private static bool IsVeryLowHP(Character target)
        {
            return target.CurrentHP < 0.3 * target.MaxHP;
        }
        private static bool NeedsHealing(Enemy target)
        {
            return target.CurrentHP < 0.8 * target.MaxHP;
        }
        private static bool CanHeal(Enemy self)
        {
            return self.Ability.Type == AbilityTargetType.Allies;
        }
        private static bool CanBuff(Enemy self)
        {
            return self.Ability.Type == AbilityTargetType.AllAllies || self.Ability.Type == AbilityTargetType.Self;
        }
        private static bool CanUseAbility(Enemy self)
        {
            return self.Ability.IsAvailable(self);
        }
        private static bool AbilityIsAttackType(Enemy self)
        {
            return self.Ability.Type == AbilityTargetType.SingleEnemy
                || self.Ability.Type == AbilityTargetType.AllEnemies
                || self.Ability.Type == AbilityTargetType.RandomEnemy;
        }
        private static Enemy? ChooseHealingTarget(BattleManager battle)
        {
            var enemies = battle.EnemySide.ActiveEnemies.Where(e => e.IsAlive && NeedsHealing(e)).ToList();
            Enemy possibleTarget = null;

            foreach (var enemy in enemies)
            {
                if (possibleTarget == null || enemy.CurrentHP < possibleTarget.CurrentHP)
                    possibleTarget = enemy;
            }
            return possibleTarget;
        }

        private static (BattleAction, Character?) WeightedChoice(Enemy self, BattleManager battle)
        {
            double roll = Random.Shared.NextDouble();

            if (roll < 0.35 && CanUseAbility(self) && AbilityIsAttackType(self))
                return (BattleAction.UseAbility, battle.Player);

            if (roll < 0.80)
                return (BattleAction.Attack, battle.Player);

            return (BattleAction.Defend, null);
        }
    }
}

using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Combat.Targeting
{
    public static class TargetingSystem
    {
        public static IEnumerable<Character> ResolveTargets(Character user,
                                                     BattleAction action,
                                                     BattleManager battle,
                                                     Character? selectedTarget = null)
        {
            var (allies, enemies) = GetTargets(user, battle);
            var targetType = GetTargetType(user, action);

            switch (targetType)
            {
                case AbilityTargetType.SingleEnemy:
                    return enemies.Contains(selectedTarget) ? new List<Character> { selectedTarget }
                                                            : new List<Character>();

                case AbilityTargetType.AllEnemies:
                    return enemies;

                case AbilityTargetType.Self:
                    return new List<Character> { user };

                case AbilityTargetType.Allies:
                    return allies.Contains(selectedTarget) ? new List<Character> { selectedTarget }
                                                           : new List<Character>();

                case AbilityTargetType.AllAllies:
                    return allies;

                case AbilityTargetType.RandomEnemy:
                    return enemies.Any() ? new List<Character> { enemies[Random.Shared.Next(enemies.Count())] }
                                         : new List<Character>();

                default:
                    return new List<Character>();
            }
        }

        public static bool TargetIsValid(Character user, 
                                         BattleAction action,
                                         BattleManager b, 
                                         Character? selectedTarget = null)
        {
            var (allies, enemies) = GetTargets(user, b);
            var targetType = GetTargetType(user, action);

            return CheckTargetType(targetType);

            bool CheckTargetType(AbilityTargetType targetType)
            {
                switch (targetType)
                {
                    case AbilityTargetType.SingleEnemy:
                        return selectedTarget != null && enemies.Contains(selectedTarget);

                    case AbilityTargetType.AllEnemies:
                        return enemies.Any();

                    case AbilityTargetType.Self:
                        return true;

                    case AbilityTargetType.Allies:
                        return selectedTarget != null && allies.Contains(selectedTarget);

                    case AbilityTargetType.AllAllies:
                        return allies.Any();

                    case AbilityTargetType.RandomEnemy:
                        return enemies.Any();

                    default:
                        return true;
                }
            }
        }

        public static AbilityTargetType GetTargetType(Character user, BattleAction action)
        {
            switch (action)
            {
                case BattleAction.Attack:
                    return AbilityTargetType.SingleEnemy;
                case BattleAction.Defend:
                    return AbilityTargetType.Self;
                case BattleAction.UseAbility:
                    return user.Ability.Type;
                default:
                    return AbilityTargetType.RandomEnemy;
            }
        }

        public static (List<Character> allies, List<Character> enemies) GetTargets(Character user, BattleManager battle)
        {
            var player = new List<Character> { battle.Player };
            var enemies = battle.EnemySide.ActiveEnemies.Where(e => e.IsAlive)
                                                        .Cast<Character>()
                                                        .ToList();

            return user == battle.Player ? (player, enemies) 
                                         : (enemies, player);
        }
    }
}

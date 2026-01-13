using Nodefall.GameLogic.Combat.Enemies;
using Nodefall.GameLogic.Combat.Targeting;
using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Nodefall.GameLogic.Combat
{
    public enum BattleState
    {
        PlayerTurn,
        EnemyTurn,
        Victory,
        Defeat,
        Retreat
    }
    public enum BattleAction
    {
        Attack,
        Defend,
        UseAbility
    }
    public class BattleManager
    {
        public Character Player { get; }
        public EnemySide EnemySide { get; }
        public BattleState State { get; private set; }
        public BattleManager(Player player, EnemySide enemySide) 
        {
            Player = player.Character;
            EnemySide = enemySide;
            State = BattleState.PlayerTurn;
        }

        public void PlayerAction(BattleAction action, Character target = null)
        {
            if (State != BattleState.PlayerTurn) return;
            //if (action == BattleAction.UseAbility && !Player.Ability.IsAvailable(Player)) return;
            //if (!TargetingSystem.TargetIsValid(Player,action,this,target)) return;

            var targets = TargetingSystem.ResolveTargets(Player, action, this, target);
            ResolveAction(Player, action, targets);

            if (CheckBattleEnd()) return;

            State = BattleState.EnemyTurn;
            Player.IsTakingAction = false;
        }

        public async Task AdvanceTurn()
        {
            if (State != BattleState.EnemyTurn) return;
            
            await EnemySide.TakeTurn(this);
            EnemySide.RefreshActiveUnits();

            if (CheckBattleEnd()) return;

            State = BattleState.PlayerTurn;
            Player.IsTakingAction = true;
        }

        public void ResolveAction(Character user, BattleAction action, IEnumerable<Character> targets)
        {
            switch (action)
            {
                case BattleAction.Attack:
                    BattleEngine.Attack(user, targets); 
                    break;
                case BattleAction.Defend:
                    BattleEngine.Defend(user);
                    break;
                case BattleAction.UseAbility:
                    BattleEngine.UseAbility(user, targets);
                    break;
            }
        }

        private bool CheckBattleEnd()
        {
            if (!EnemySide.HasLivingUnits)
            {
                State = BattleState.Victory;
                return true;
            }

            if (!Player.IsAlive)
            {
                State = BattleState.Defeat;
                return true;
            }

            return false;
        }

        public void Retreat ()
        {
            if (State == BattleState.Victory || State == BattleState.Defeat) return;

            State = BattleState.Retreat;
        }
    }
}

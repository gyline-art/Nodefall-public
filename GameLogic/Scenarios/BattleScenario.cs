using Nodefall.GameLogic.Combat;
using Nodefall.GameLogic.Combat.Enemies;
using Nodefall.Interfaces;
using Nodefall.Models;
using Nodefall.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nodefall.GameLogic.Scenarios
{
    public class BattleScenario : IScenario
    {
        public virtual string Name => "Battle";
        public Encounter Encounter { get; }

        public BattleScenario(Encounter encounter)
        {
            Encounter = encounter;
        }

        public void Execute(GameManager game)
        {
            var enemies = EnemyFactory.CreateEncounter(Encounter, game.DiffiultyLevel);
            var enemySide = new EnemySide(enemies);
            var battle = new BattleManager(game.Player, enemySide);

            BattleWindow window = new BattleWindow(battle);
            window.ShowDialog();

            switch(battle.State)
            {
                case BattleState.Victory:
                    HandleVictory(game, enemySide);
                    break;
                case BattleState.Defeat:
                    HandleDefeat(game);
                    break;
                case BattleState.Retreat:
                    HandleRetreat(game);
                    break;
            }
        }

        protected virtual void HandleVictory(GameManager game, EnemySide side)
        {
            GrantRewards(game, side);
        }
        protected virtual void HandleRetreat(GameManager game)
        {
            ApplyPenalty(game);
        }
        private void HandleDefeat(GameManager game)
        {
            //MessageBox.Show("You have fallen...", "Defeat");
            game.EndGame();
        }

        private void GrantRewards(GameManager game, EnemySide side)
        {
            var (gold, exp) = side.GetFinalRewards();
            game.Player.Gold += gold;
            game.Player.Exp += exp;

            string rewardMessage = "Rewards Obtained:" +
                                  $"\n Gold: +{gold}" +
                                  $"\n  Exp: +{exp}";

            MessageBox.Show(rewardMessage, "Rewards");
        }
        private void ApplyPenalty(GameManager game)
        {
            game.AdvanceDifficulty();
            // other penalties (gold/exp loss)
        }
    }
}

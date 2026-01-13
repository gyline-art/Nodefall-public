using Nodefall.GameLogic.Combat.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Scenarios
{
    public class BossScenario : BattleScenario
    {
        public override string Name => "Boss";
        public BossScenario(Encounter encounter) : base(encounter)
        {

        }

        protected override void HandleVictory(GameManager game, EnemySide side)
        {
            AdvanceGame(game);
            base.HandleVictory(game, side);
        }
        protected override void HandleRetreat(GameManager game)
        {
            base.HandleRetreat(game);
            AdvanceGame(game);
        }

        private void AdvanceGame(GameManager game)
        {
            game.Player.CurrentNode = null;
            game.AdvanceDifficulty();
            game.GenerateNewMap();
            game.GameWindow.RenderNewMap();
        }
    }
}

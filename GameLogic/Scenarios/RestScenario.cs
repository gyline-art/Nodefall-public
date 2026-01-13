using Nodefall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Scenarios
{
    public class RestScenario : IScenario
    {
        public string Name => "Rest";
        public void Execute(GameManager game)
        {
            game.Player.Character.CurrentHP = game.Player.Character.MaxHP;
            game.Player.Character.CurrentMP = game.Player.Character.MaxMP;
            game.Player.Gold += 5;
            game.Player.Exp += 5;
        }
    }
}

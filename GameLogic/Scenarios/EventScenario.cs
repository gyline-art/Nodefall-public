using Nodefall.GameData;
using Nodefall.Interfaces;
using Nodefall.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Scenarios
{
    public class EventScenario : IScenario
    {
        public string Name => "Event";
        public void Execute(GameManager game)
        {
            var cards = EventCardLibrary.Draw(3);
            var window = new EventWindow(game, cards);
            window.ShowDialog();
        }
    }
}

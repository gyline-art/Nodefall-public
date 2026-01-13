using Nodefall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Map
{
    public class Node
    {
        public int X { get; }
        public int Y { get; }
        
        public bool IsVisited { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsFallen { get; set; }
        public IScenario Scenario { get; set; }

        public Node( int x, int y) 
        {
            X = x;
            Y = y;
            IsVisited   = false;
            IsAvailable = false;
            IsFallen    = false;
        }
    }
}

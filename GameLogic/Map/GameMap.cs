using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Map
{
    public class GameMap
    {
        public Node[,] Nodes { get; }
        public int Width { get; }
        public int Height { get; }

        public GameMap(int width, int height) 
        {
            Width = width;
            Height = height;
            Nodes = new Node[width, height];
        }

        public Node GetStarterNode()
        {
            return Nodes[0, Height / 2];
        }
    }
}

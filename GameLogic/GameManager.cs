using Nodefall.GameLogic.Map;
using Nodefall.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic
{
    public class GameManager
    {
        public Player Player { get; }
        public GameMap Map { get; private set; }
        public int DiffiultyLevel { get; private set; }
        public bool IsGameOver => !Player.Character.IsAlive;

        public GameWindow GameWindow { get; private set; }

        public GameManager(Player player, int difficulty = 1)
        {
            Player = player;
            DiffiultyLevel = difficulty;
        }

        public void StartNewGame()
        {
            GenerateNewMap();
            GameWindow = new GameWindow(this);
            GameWindow.ShowDialog();
        }

        public void CommitToNodePath(Node node) 
        {
            if (!CanEnter(node)) return;

            for (int y = 0; y < Map.Height; y++)
            {
                var sn = Map.Nodes[node.X, y];
                if (sn != node && sn != null)
                {
                    sn.IsFallen = true;
                }
            }
        }

        public void EnterNode(Node node)
        {
            if (!CanEnter(node)) return;

            if (Player.CurrentNode != null)
                Player.CurrentNode.IsVisited = true;

            Player.CurrentNode = node;
            node.IsAvailable = false;

            node.Scenario.Execute(this);
            if (IsGameOver) EndGame();

            UpdateAvailableNodes();
        }

        private void UpdateAvailableNodes() 
        {
            if (Player.CurrentNode == null) return;
            int CurrentX = Player.CurrentNode.X;

            foreach (var node in Map.Nodes)
            {
                if (node == null) continue;

                if (node.X == CurrentX + 1)
                    node.IsAvailable = true;
                else
                    node.IsAvailable = false;
            }
        }

        public void GenerateNewMap()
        {
            Map = new GameMap(4 + DiffiultyLevel, 3);
            MapGenerator.GenerateMap(Map);
        }

        public void AdvanceDifficulty()
        {
            DiffiultyLevel++;
        }

        public void EndGame()
        {
            GameWindow.Close();
        }

        private bool CanEnter(Node node)
        {
            return node.IsAvailable && !node.IsVisited && !node.IsFallen;
        }
    }
}

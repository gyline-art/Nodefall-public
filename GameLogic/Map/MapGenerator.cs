using Nodefall.GameData;
using Nodefall.GameLogic.Combat.Enemies;
using Nodefall.GameLogic.Scenarios;
using Nodefall.Helpers;
using Nodefall.Windows;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Nodefall.GameLogic.Map
{
    public static class MapGenerator
    {
        private static readonly Random Random = new Random();

        public static void GenerateMap(GameMap map)
        {
            GenerateLayout(map);
            AssignScenarios(map);
        }

        private static void GenerateLayout(GameMap map)
        {
            int centerY = map.Height / 2;

            for (int i = 0; i < map.Width; i++) 
            {
                CreateNode(map, i, centerY);

                if (i == 0 || i == map.Width - 1)
                    continue;

                int roll = Random.Next(5);
                if (roll == 1 || roll == 3)
                    CreateNode(map, i, centerY - 1);
                if (roll == 2 || roll == 3)
                    CreateNode(map, i, centerY + 1);
            }
        }

        private static void CreateNode(GameMap map, int x, int y)
        {
            if (map.Nodes[x,y] == null)
                map.Nodes[x,y] = new Node(x,y);
        }

        private static void AssignScenarios(GameMap map)
        {
            foreach (var node in map.Nodes)
            {
                if (node == null) continue;

                if (node.X == 0)
                {
                    node.Scenario = new RestScenario();
                    node.IsAvailable = true;
                }
                else if (node.X == map.Width - 1)
                {
                    var encounter = RandomUtil.PickOne(EncounterLibrary.Boss);
                    node.Scenario = new BossScenario(encounter);
                }
                else
                {
                    int roll = Random.Next(2);
                    switch (roll)
                    {
                        case 0:
                            var encounter = RandomUtil.PickOne(EncounterLibrary.Basic);
                            node.Scenario = new BattleScenario(encounter);
                            break;
                        case 1:
                            node.Scenario = new EventScenario();
                            break;
                    }
                }
            }
        }
    }
}

using Nodefall.GameLogic;
using Nodefall.GameLogic.Map;
using Nodefall.Models;
using Nodefall.Windows.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nodefall.Windows
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameManager Manager { get; }
        public Node CurrentNode => Manager.Player.CurrentNode;
        public GameWindow(GameManager game)
        {
            Manager = game;
            InitializeComponent();
            RenderNewMap();
            AddPlayerCharacterSheet();
            Closing += GameWindow_Closing;
        }
        private void GameWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        public async void RenderNewMap()
        {
            MapPanel.Children.Clear();

            var map = Manager.Map;

            for (int x = 0; x < map.Width; x++)
            {
                var column = new MapColumnPanel(x);

                for (int y = 0; y < map.Height; y++)
                {
                    var node = map.Nodes[x, y];
                    if (node == null) continue;

                    var button = new NodeButton(node);
                    button.Click += Node_Button_Click;
                    column.Children.Add(button);
                }

                MapPanel.Children.Add(column);
            }

            await RefreshMapStateAsync();
        }

        private async Task RefreshMapStateAsync()
        {
            var fallenNodes = new List<NodeButton>();
            ;
            foreach (var column in MapPanel.Children.OfType<MapColumnPanel>())
            {
                foreach (var button in column.Children.OfType<NodeButton>())
                {
                    var node = button.Node;

                    button.IsEnabled = node.IsAvailable && !node.IsVisited && !node.IsFallen && node != CurrentNode;

                    if (node.IsFallen)
                    {
                        fallenNodes.Add(button);
                        continue;
                    }

                    // apply state visuals
                    if (node.IsAvailable || node == CurrentNode)
                        button.Opacity = 1.0;
                    else if (node.IsVisited)
                        button.Opacity = 0.4;
                    else if (!node.IsAvailable)
                        button.Opacity = 0.7;
                }
                
                if (fallenNodes.Any())
                {
                    await NodeAnimations.AnimateFallenNodesAsync(fallenNodes);
                    foreach (var node in fallenNodes)
                    {
                        column.Children.Remove(node);
                    }
                    fallenNodes.Clear();
                }
            }
        }

        private void AddPlayerCharacterSheet()
        {
            CharacterPanel.Children.Add(new CharacterSheet(Manager.Player.Character));
        }

        private async void Node_Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is NodeButton nb)
            {
                Manager.CommitToNodePath(nb.Node);
                await RefreshMapStateAsync(); // wait for fall animation
                Manager.EnterNode(nb.Node);
                await RefreshMapStateAsync(); // display new available nodes 
            }
        }
    }
}

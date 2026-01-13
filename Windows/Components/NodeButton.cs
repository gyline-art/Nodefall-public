using Nodefall.GameLogic.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nodefall.Windows.Components
{
    public class NodeButton : Button
    {
        public Node Node { get; }

        public NodeButton(Node node)
        {
            Node = node;
            Height = 80;
            Width  = 80;
            Margin = new Thickness(6);
            Content = node.Scenario.Name;
        }
    }
}

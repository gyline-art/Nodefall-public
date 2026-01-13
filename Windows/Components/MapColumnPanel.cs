using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nodefall.Windows.Components
{
    public class MapColumnPanel : StackPanel
    {
        public int ColumnIndex { get; }

        public MapColumnPanel(int x)
        {
            ColumnIndex = x;
            Orientation = Orientation.Vertical;
            VerticalAlignment = VerticalAlignment.Center;
            Margin = new Thickness(20,0,20,0);
        }
    }
}

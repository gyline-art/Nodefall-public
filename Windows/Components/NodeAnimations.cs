using Nodefall.GameLogic.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Nodefall.Windows.Components
{
    public static class NodeAnimations
    {
        public static Task AnimateFallenNodesAsync(List<NodeButton> nodes)
        {
            var tasks = new List<Task>();

            foreach (var node in nodes)
            {
                tasks.Add(FallAndFadeAsync(node));
            }

            return Task.WhenAll(tasks);
        }


        private static Task FallAndFadeAsync(NodeButton node)
        {
            var tsc = new TaskCompletionSource();

            int completed = 0;
            void OnCompleted(object? sender, EventArgs e)
            {
                completed++;
                if (completed == 2) tsc.SetResult();
            }

            var fade = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
            fade.Completed += OnCompleted;

            var move = new ThicknessAnimation(new Thickness(0), new Thickness(0, 40, 0, 0), TimeSpan.FromMilliseconds(300));
            move.Completed += OnCompleted;

            node.BeginAnimation(UIElement.OpacityProperty, fade);
            node.BeginAnimation(FrameworkElement.MarginProperty, move);

            return tsc.Task;
        }
    }
}

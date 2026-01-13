using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Nodefall.Windows.Components
{
    public static class CharacterSheetAnimations
    {
        public static void PlayDeathFade(CharacterSheet c)
        {
            var fade = new DoubleAnimation
            {
                From = 1,
                To = 0.3,
                Duration = TimeSpan.FromMilliseconds(400),
                FillBehavior = FillBehavior.HoldEnd
            };
            c.BeginAnimation(UIElement.OpacityProperty, fade);
        }

        public static void PlayDefendGlow(CharacterSheet c)
        {
            if (!c.Character.IsDefending) return;

            var grow = new DoubleAnimation
            {
                From = 1,
                To = 1.2,
                Duration = TimeSpan.FromMilliseconds(200),
                AutoReverse = true
            };

            c.RenderTransform = new ScaleTransform(1, 1);

            c.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, grow);
            c.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, grow);
        }

        public static void PlayHealOrHitFlash(CharacterSheet c, int lastHP)
        {
            var flash = new ColorAnimation
            {
                From = Colors.Transparent,
                To = c.Character.CurrentHP >= lastHP ? Colors.LightGreen
                                                    : Colors.IndianRed,
                Duration = TimeSpan.FromMilliseconds(150),
                AutoReverse = true
            };

            var brush = new SolidColorBrush(Colors.Transparent);
            c.Background = brush;

            brush.BeginAnimation(SolidColorBrush.ColorProperty, flash);
        }

        public static void PlayActionHighlight(CharacterSheet c)
        {
            if (c.Character == null) return;

            c.RenderTransform = new ScaleTransform(1, 1);
            double targetScale = c.Character.IsTakingAction ? 1.1 : 1;

            var highlightGrow = new DoubleAnimation
            {
                To = targetScale,
                Duration = TimeSpan.FromMilliseconds(200),
            };

            c.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, highlightGrow);
            c.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, highlightGrow);

        }
    }
}

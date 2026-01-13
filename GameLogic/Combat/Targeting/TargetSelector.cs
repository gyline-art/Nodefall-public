using Nodefall.Windows;
using Nodefall.Models;
using Nodefall.Windows.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Combat.Targeting
{
    public static class TargetSelector
    {
        public static void UpdateTargets(BattleWindow window, BattleAction action)
        {
            var sheets = window.GetCharacterSheets();

            foreach (var sheet in sheets)
            {
                sheet.IsSelectable = false;
                sheet.IsSelected = false;
            }

            var targetType = TargetingSystem.GetTargetType(window.Player, action);

            switch (targetType)
            {
                case AbilityTargetType.SingleEnemy:
                case AbilityTargetType.RandomEnemy:
                    SelectSheets(window, false);
                    break;

                case AbilityTargetType.AllEnemies:
                    SelectSheets(window, false, true);
                    break;

                case AbilityTargetType.Self:
                case AbilityTargetType.Allies:
                case AbilityTargetType.AllAllies:
                    SelectSheets(window, true);
                    break;
            }
        }

        private static void SelectSheets(BattleWindow window, bool isTargetPlayer, bool selectAll = false)
        {
            List<CharacterSheet> sheets = isTargetPlayer ? window.PlayerLineUp.Children.OfType<CharacterSheet>().ToList()
                                                        : window.EnemyLineUp.Children.OfType<CharacterSheet>().ToList();
            bool foundSelected = false;
            foreach (var sheet in sheets)
            {
                sheet.IsSelectable = !selectAll;
                if (sheet.Character == window.SelectedCharacter)
                {
                    sheet.IsSelected = true;
                    foundSelected = true;
                }
                else sheet.IsSelected = selectAll;
            }

            if (!selectAll && sheets.Any() && !foundSelected)
                sheets.First().IsSelected = true;
            foundSelected = false;
        }
    }
}

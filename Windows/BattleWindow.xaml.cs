using Nodefall.GameLogic.Combat;
using Nodefall.GameLogic.Combat.Targeting;
using Nodefall.Models;
using Nodefall.Windows.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static System.Collections.Specialized.BitVector32;

namespace Nodefall.Windows
{
    /// <summary>
    /// Interaction logic for BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window, INotifyPropertyChanged
    {
        private Character _selectedCharacter;
        public BattleManager Battle { get; }
        public Character Player => Battle.Player;
        public Character SelectedCharacter 
        {
            get => _selectedCharacter;
            set
            {
                _selectedCharacter = value;
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }
        public List<Enemy> ActiveEnemies => Battle.EnemySide.ActiveEnemies;
        public BattleState CurrentState => Battle.State;
        public BattleAction CurrentAction {  get; set; }
        public bool IsPlayerTurn => CurrentState == BattleState.PlayerTurn;
        public bool IsBattleOver =>
            CurrentState == BattleState.Victory ||
            CurrentState == BattleState.Defeat  ||
            CurrentState == BattleState.Retreat ;

        public string GameAnnouncement
        {
            get
            {
                switch (CurrentState)
                {
                    case BattleState.PlayerTurn:
                        return "Your Turn";
                    case BattleState.EnemyTurn:
                        return "Enemy Turn";
                    case BattleState.Victory:
                        return "Victory";
                    case BattleState.Defeat:
                        return "You have fallen...";
                    case BattleState.Retreat:
                        return "You fled...";
                }
                return CurrentState.ToString();
            }
        }

        public BattleWindow(BattleManager battle)
        {
            Battle = battle;
            DataContext = this;
            InitializeComponent();
            BuildLineUps();
            Player.IsTakingAction = true;
            Closing += BattleWindow_Closing;
        }

        public List<CharacterSheet> GetCharacterSheets()
        {
            List<CharacterSheet> enemySheets = EnemyLineUp.Children.OfType<CharacterSheet>().ToList();
            List<CharacterSheet> playerSheets = PlayerLineUp.Children.OfType<CharacterSheet>().ToList();

            enemySheets.AddRange(playerSheets);

            return enemySheets;
        }

        private void BattleWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsBattleOver)
            {
                Battle.Retreat();
            }
        }

        private void Attack_Button_Click(object sender, RoutedEventArgs e)
        {
            var action = BattleAction.Attack;
            if (CurrentAction != action || !IsPlayerTurn || 
                !TargetingSystem.TargetIsValid(Player, action, Battle, SelectedCharacter))
            {
                SetAction(action);
                return;
            }

            Battle.PlayerAction(action, SelectedCharacter);
            RefreshBattleUI();
            AdvanceTurn();
        }

        private void Defend_Button_Click(object sender, RoutedEventArgs e)
        {
            var action = BattleAction.Defend;
            if (CurrentAction != action || !IsPlayerTurn)
            {
                SetAction(action);
                return;
            }

            Battle.PlayerAction(BattleAction.Defend);
            RefreshBattleUI();
            AdvanceTurn();
        }

        private void Ability_Button_Click(object sender, RoutedEventArgs e)
        {
            var action = BattleAction.UseAbility;
            if (CurrentAction != action || !IsPlayerTurn || !Player.Ability.IsAvailable(Player) ||
                !TargetingSystem.TargetIsValid(Player, action, Battle, SelectedCharacter))
            {
                SetAction(action);
                return;
            }

            Battle.PlayerAction(action, SelectedCharacter);
            RefreshBattleUI();
            AdvanceTurn();
        }

        private void Retreat_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsPlayerTurn) return;
            Battle.Retreat();
            this.Close();
        }

        private void BattleResult_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsBattleOver) return; 
            Close(); 
            // Bind to Anouncer_Button together with the GameAnnouncement
            // + Add Notify to everything xD
        }

        private void BuildLineUps()
        {
            PlayerLineUp.Children.Clear();  
            EnemyLineUp.Children.Clear();

            var playerSheet = new CharacterSheet(Player);

            playerSheet.SheetSelected += c =>
            {
                SelectedCharacter = c;
                UpdateSelectionVisuals();
            };

            PlayerLineUp.Children.Add(playerSheet);


            foreach (var enemy in ActiveEnemies)
            {
                var sheet = new CharacterSheet(enemy);

                sheet.SheetSelected += c =>
                {
                    SelectedCharacter = c;
                    UpdateSelectionVisuals();
                };

                EnemyLineUp.Children.Add(sheet);
            }
        }

        private async void AdvanceTurn()
        {
            await Battle.AdvanceTurn();
            RefreshBattleUI();
            RefreshEnemies();
        }

        private void RefreshBattleUI()
        {
            OnPropertyChanged(nameof(CurrentState));
            OnPropertyChanged(nameof(IsPlayerTurn));
            OnPropertyChanged(nameof(IsBattleOver));
            OnPropertyChanged(nameof(GameAnnouncement));
        }

        private void RefreshEnemies()
        {
            OnPropertyChanged(nameof(ActiveEnemies));

            var deadSheets = EnemyLineUp.Children.OfType<CharacterSheet>()
                                                  .Where(s => !s.Character.IsAlive)  
                                                  .ToList();
            foreach (var sheet in deadSheets)
            {
                EnemyLineUp.Children.Remove(sheet);
            }

            for (int i = EnemyLineUp.Children.Count; i < ActiveEnemies.Count(); i++)
            {
                var nextEnemy = ActiveEnemies[i];
                var nextSheet = new CharacterSheet(nextEnemy);

                nextSheet.SheetSelected += c =>
                {
                    SelectedCharacter = c; 
                    UpdateSelectionVisuals();
                };

                EnemyLineUp.Children.Add(nextSheet);
            }
        }

        private void SetAction(BattleAction action)
        {
            CurrentAction = action;
            TargetSelector.UpdateTargets(this, action);
        }

        private void UpdateSelectionVisuals()
        {
            foreach (var sheet in GetCharacterSheets())
            {
                sheet.IsSelected = SelectedCharacter == sheet.Character;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

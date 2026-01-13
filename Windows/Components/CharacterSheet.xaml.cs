using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nodefall.Windows.Components
{
    /// <summary>
    /// Interaction logic for CharacterSheet.xaml
    /// </summary>
    public partial class CharacterSheet : UserControl
    {
        private Character _character;
        private int _lastHP;
        public Character Character
        {
            get => (Character)GetValue(CharacterProperty);

            set
            {
                var old = _character;
                if (old != null)
                {
                    old.PropertyChanged -= Character_PropertyChanged;
                }

                _character = value;
                _lastHP = _character.CurrentHP;
                if (_character != null)
                {
                    _character.PropertyChanged += Character_PropertyChanged;
                }

                SetValue(CharacterProperty, value);
            } 
        }
        public bool IsSelectable 
        {
            get => (bool)GetValue(IsSelectableProperty);
            set => SetValue(IsSelectableProperty, value);
        }
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
        public event Action<Character>? SheetSelected;

        public static readonly DependencyProperty CharacterProperty = DependencyProperty.Register(nameof(Character),
                                                                                                  typeof(Character),
                                                                                                  typeof(CharacterSheet),
                                                                                                  new PropertyMetadata(null));

        public static readonly DependencyProperty IsSelectableProperty = DependencyProperty.Register(nameof(IsSelectable),
                                                                                                   typeof(bool),
                                                                                                   typeof(CharacterSheet),
                                                                                                   new PropertyMetadata(null));

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected),
                                                                                                   typeof(bool),
                                                                                                   typeof(CharacterSheet),
                                                                                                   new PropertyMetadata(null));

        public CharacterSheet(Character character)
        {
            Character = character;
            InitializeComponent();
            DataContext = this;
            IsSelectable = true;
        }

        private void Sheet_Click(object sender, RoutedEventArgs e)
        {
            if (!IsSelectable)
            {
                MessageBox.Show("Cannot Select");
                return;
            }
            SheetSelected?.Invoke(Character);
        }

        private void Character_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Character.IsAlive) && !_character.IsAlive)
            {
                CharacterSheetAnimations.PlayDeathFade(this);
            }

            if (e.PropertyName == nameof(Character.CurrentHP))
            {
                CharacterSheetAnimations.PlayHealOrHitFlash(this, _lastHP);
            }

            if (e.PropertyName == nameof(Character.IsDefending))
            {
                CharacterSheetAnimations.PlayDefendGlow(this);
            }

            if (e.PropertyName == nameof(Character.IsTakingAction))
            {
                CharacterSheetAnimations.PlayActionHighlight(this);
            }
        }
    }
}
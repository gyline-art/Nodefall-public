using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameData
{
    public class CharacterManager : INotifyPropertyChanged
    {
        private Character _selectedCharacter;
        public Character SelectedCharacter
        {
            get => _selectedCharacter;
            set
            {
                _selectedCharacter = value;
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }

        public ObservableCollection<Character> Characters { get; set; }
        public CharacterManager()
        {
            Characters = new ObservableCollection<Character>
            {
                new Character("Wizard", CharacterAssets.Caster, CharacterAssets.Fireball),
                new Character("Scout", CharacterAssets.Assassin, CharacterAssets.Counter)
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

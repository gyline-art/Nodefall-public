using Nodefall.GameData;
using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for CustomizeWindow.xaml
    /// </summary>
    public partial class CustomizeWindow : Window, INotifyPropertyChanged
    {
        private Character _selectedCharacter;
        private Character _editCopy;
        private string _editCopyAttributes;
        public CharacterManager Manager { get; }
        public List<Archetype> Archetypes => CharacterAssets.Archetypes;
        public List<Ability> Abilities => CharacterAssets.Abilities;
        public Character SelectedCharacter
        {
            get => _selectedCharacter;
            set
            {
                _selectedCharacter = value;

                if (value != null)
                    EditCopy = new Character(value.Name, value.Archetype, value.Ability);
                else
                    EditCopy = new Character();

                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }
        public Character EditCopy
        {
            get => _editCopy;
            set
            {
                _editCopy = value;

                EditCopyAttributes = _editCopy != null ? AttributeFormatter.FormatCharacter(_editCopy)
                                                       : string.Empty;

                OnPropertyChanged(nameof(EditCopy));
            }
        }
        public string EditCopyAttributes
        {
            get => _editCopyAttributes;
            set
            {
                _editCopyAttributes = value;
                OnPropertyChanged(nameof(EditCopyAttributes));
            }
        }

        public CustomizeWindow(CharacterManager manager)
        {
            InitializeComponent();
            Manager = manager;
            EditCopy = new Character();
            DataContext = this;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsNotValid(EditCopy)) return;

            Manager.Characters.Add(Copy(EditCopy));
            SelectedCharacter = null;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsNotValid(EditCopy)) return;

            if (SelectedCharacter != null)
            {
                SelectedCharacter.Name      = EditCopy.Name;
                SelectedCharacter.Archetype = EditCopy.Archetype;
                SelectedCharacter.Ability   = EditCopy.Ability;
            }
            else
            {
                Manager.Characters.Add(Copy(EditCopy));
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCharacter == null) return;

            Manager.Characters.Remove(SelectedCharacter);
            SelectedCharacter = null;
        }

        private Character Copy(Character c) =>
            new Character(c.Name, c.Archetype, c.Ability);
        private bool IsNotValid(Character c) =>
            c == null ||
            string.IsNullOrWhiteSpace(c.Name) ||
            c.Archetype == null ||
            c.Ability == null ||
            NameAlreadyExists(c);
        private bool NameAlreadyExists(Character c)
        {
            return Manager.Characters.Any(x => x.Name == c.Name && x != SelectedCharacter);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

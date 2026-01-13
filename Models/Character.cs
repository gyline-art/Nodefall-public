using Nodefall.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models
{
    public static class BaseStats
    {
        public const int BaseHp  = 20;
        public const int BaseMp  =  5;
        public const int BaseAtk = 10;
    }

    public class Character : INotifyPropertyChanged
    {
        private string _name;
        private Archetype _archetype;
        private Ability _ability;
        private int _currentHP;
        private int _currentMP;
        private bool _isDefending;
        private bool _isTakingAction;
        public int MaxHP { get; set; }
        public int MaxMP { get; set; }
        public int ATK { get; set; }
        public List<IAdvantage> AdvantageSources { get; } = new();
        public List<IAdvantage> AdvantageTargets { get; } = new();
        public bool IsAlive => CurrentHP > 0;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public Archetype Archetype
        {
            get => _archetype;
            set
            {
                if (_archetype != null) 
                    _archetype.RemoveFrom(this);
                _archetype = value;
                _archetype.ApplyTo(this);
                _archetype.RecalculateStats(this);
                OnPropertyChanged(nameof(Archetype));
                OnPropertyChanged(nameof(MaxHP));
                OnPropertyChanged(nameof(ATK));
            }
        }

        public Ability Ability
        {
            get => _ability;
            set
            {
                _ability = value;
                OnPropertyChanged(nameof(Ability));
            }
        }

        public int CurrentHP
        {
            get => _currentHP;
            set
            {
                bool wasAlive = IsAlive;
                _currentHP = value;
                OnPropertyChanged(nameof(CurrentHP));

                if (wasAlive && !IsAlive)
                    OnPropertyChanged(nameof(IsAlive));
            }
        }

        public int CurrentMP
        {
            get => _currentMP;
            set
            {
                _currentMP = value;
                OnPropertyChanged(nameof(CurrentMP));
            }
        }

        public bool IsDefending
        {
            get => _isDefending;
            set
            {
                _isDefending = value;
                OnPropertyChanged(nameof(IsDefending));
            }
        }

        public bool IsTakingAction
        {
            get => _isTakingAction;
            set
            {
                _isTakingAction = value;
                OnPropertyChanged(nameof(IsTakingAction));
            }
        }

        public Character() { }
        public Character(string name, Archetype archetype, Ability ability)
        {
            Name = name;
            Archetype = archetype;
            Ability = ability;
            IsTakingAction = false;
        }

        public virtual bool Hit(int hitValue = 0)
        {
            if (IsDefending)
            {
                hitValue = (int)Math.Round((float)hitValue / 3);
                IsDefending = false;
            }

            if (CurrentHP - hitValue < 0) CurrentHP = 0;
            else CurrentHP -= hitValue;


            return true;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

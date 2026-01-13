using Nodefall.GameLogic.Map;
using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic
{
    public class Player
    {
        private int _lvl = 0;
        private int _exp = 0;
        private int _expNeeded = 10;
        public Character Character { get; }
        public Node CurrentNode { get; set; }

        public int Gold { get; set; } = 0;
        public int Lvl 
        {
            get => _lvl;
            set
            {
                _lvl = value;
                ApplyLvlRewards();
            }
        }
        public int Exp 
        {
            get => _exp;
            set
            {
                _exp = value;
                AttemptLvlUp();
            }
        }

        public Player(Character c) 
        {
            var ability = c.Ability.CreateCopy();
            Character = new Character(c.Name, c.Archetype, ability);
        }

        private void AttemptLvlUp()
        {
            while (Exp >= _expNeeded)
            {
                _exp -= _expNeeded;
                Lvl++;
                _expNeeded = (int)Math.Round(_expNeeded * 1.5);
            }
        }
        private void ApplyLvlRewards()
        {
            var hp  = BaseStats.BaseHp + Character.Archetype.BonusHP;
            var mp  = BaseStats.BaseMp + Character.Archetype.BonusMP;
            var atk = BaseStats.BaseAtk + Character.Archetype.BonusATK;

            Character.MaxHP += (int)Math.Ceiling((float)(hp) * Lvl / 10);
            Character.MaxMP += (int)Math.Ceiling((float)(mp) * Lvl / 10);
            Character.ATK  += (int)Math.Ceiling((float)(atk) * Lvl / 10);

            Character.CurrentHP = Character.MaxHP;
            Character.CurrentMP = Character.MaxMP;
        }
    }
}

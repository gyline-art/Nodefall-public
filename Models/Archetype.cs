using Nodefall.GameLogic.Combat.Advantage;
using Nodefall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Nodefall.Models
{
    public abstract class Archetype : AdvantageSource
    {
        public abstract string Name { get; }
        public virtual int BonusHP => 0;
        public virtual int BonusATK => 0;
        public virtual int BonusMP => 0;
        public virtual void RecalculateStats(Character c)
        {
            c.MaxHP = BaseStats.BaseHp  + BonusHP;
            c.MaxMP = BaseStats.BaseMp  + BonusMP;
            c.ATK   = BaseStats.BaseAtk + BonusATK;
            c.CurrentHP = c.MaxHP;
            c.CurrentMP = c.MaxMP;
        }
        public override string ToString() => Name;
    }
}

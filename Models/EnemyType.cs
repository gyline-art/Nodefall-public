using Nodefall.GameLogic.Combat.Advantage;
using Nodefall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models
{
    public abstract class EnemyType : AdvantageSource, IRewardSource
    {
        public abstract string Name { get; }
        public abstract int BaseHP { get; }
        public abstract int BaseMP { get; }
        public abstract int BaseATK { get; }
        public abstract float DifficultyScaling {  get; }

        public abstract int Gold { get; }
        public abstract int Exp { get; }

        public void ApplyFinalStats(Enemy e)
        {
            e.MaxHP = ScaleStat(BaseHP  + e.Archetype.BonusHP,  e.Difficulty);
            e.MaxMP = ScaleStat(BaseMP  + e.Archetype.BonusMP,  e.Difficulty);
            e.ATK   = ScaleStat(BaseATK + e.Archetype.BonusATK, e.Difficulty);
            e.CurrentHP = e.MaxHP;
            e.CurrentMP = e.MaxMP;
        }

        private int ScaleStat(int baseStat, int difficulty)
        {
            return (int)Math.Round(baseStat * difficulty * DifficultyScaling);
        }
    }
}

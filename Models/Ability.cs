using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models
{
    public enum AbilityTargetType
    {
        SingleEnemy,    // High ST DMG
        AllEnemies,     // Low AOE DMG
        Self,           // Buff
        Allies,         // Heal
        AllAllies,      // Low Buff / Heal
        RandomEnemy     // Situational
    }

    public abstract class Ability
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract AbilityTargetType Type { get; }
        public abstract void Use(Character user, IEnumerable<Character> targets);
        public virtual bool IsAvailable(Character user)
        { 
            return user.CurrentMP - 1 > 0;  
        }
        public abstract Ability CreateCopy();
        public override string ToString() => Name;
    }
}

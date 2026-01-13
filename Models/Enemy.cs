using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models
{
    public class Enemy : Character
    {
        public EnemyType Type { get; }
        public int Difficulty { get; set; }
        public Enemy (EnemyType type, Archetype archetype, Ability ability, string? enemyName = null, int difficulty = 1)
        {
            Name = enemyName ?? type.Name;
            Ability = ability;
            Archetype = archetype;
            Type = type;
            Difficulty = difficulty;                                   
            type.ApplyTo (this);
            type.ApplyFinalStats(this);
        }
    }
}

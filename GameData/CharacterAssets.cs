using Nodefall.Models;
using Nodefall.Models.Abilities;
using Nodefall.Models.Archetypes;
using Nodefall.Models.EnemyTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameData
{
    public static class CharacterAssets
    {
        // Abilities
        public static readonly Fireball Fireball = new Fireball();
        public static readonly Counter Counter = new Counter();
        public static readonly Tackle ShieldBash = new Tackle();
        public static readonly Heal Heal = new Heal();

        public static readonly List<Ability> Abilities = new List<Ability>()
        {
            Fireball, ShieldBash, Heal
        };

        // Archetypes
        public static readonly Warrior Warrior = Warrior.Instance;
        public static readonly Shaman Caster = Shaman.Instance;
        public static readonly Assassin Assassin = Assassin.Instance;
        public static readonly Berserker Berserker = Berserker.Instance;

        public static readonly List<Archetype> Archetypes = new List<Archetype>()
        {
            Warrior, Caster, Assassin, Berserker
        };

        // Enemy Types
        public static readonly List<EnemyType> EnemyTypes = new List<EnemyType>()
        {
            Goblin.Instance,
            Skeleton.Instance,
            Beastman.Instance,
            Chimera.Instance
        };
    }
}

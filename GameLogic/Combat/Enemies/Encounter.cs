using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Combat.Enemies
{
    public class EnemyPreset
    {
        public EnemyType EnemyType;
        public Archetype Archetype;
        public Ability Ability;
        public string Name;
        public EnemyPreset(EnemyType type, Archetype archetype, Ability ability, string name = null)
        {
            EnemyType = type;
            Archetype = archetype;
            Ability = ability;
            Name = name;
        }
    }

    public class Encounter
    {
        public int ID { get; }
        public string Name { get; }
        public int MinEnemies { get; }
        public int MaxEnemies { get; }
        public bool UseOnlyPresets { get; }

        public IReadOnlyList<EnemyType> EnemyTypes { get; }
        public IReadOnlyList<Archetype> Archetypes { get; }
        public IReadOnlyList<Ability> Abilities { get; }
        public IReadOnlyList<EnemyPreset> Presets { get; }
        public Encounter(int id,
                          string name,
                          int minEnemies,
                          int maxEnemies,
                          IEnumerable<EnemyType> types,
                          IEnumerable<Archetype> archetypes,
                          IEnumerable<Ability> abilities,
                          IEnumerable<EnemyPreset>? presets = null,
                          bool useOnlyPresets = false )
        {
            ID = id;
            Name = name;
            MinEnemies = minEnemies;
            MaxEnemies = maxEnemies;
            EnemyTypes = types.ToList();
            Archetypes = archetypes.ToList();
            Abilities = abilities.ToList();
            Presets = presets != null ? presets.ToList() : new();
            UseOnlyPresets = useOnlyPresets;
        }
    }
}

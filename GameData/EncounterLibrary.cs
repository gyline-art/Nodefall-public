using Nodefall.GameLogic.Combat.Enemies;
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
    public static class EncounterLibrary
    {
        public static IReadOnlyList<Encounter> Basic { get; } = new List<Encounter>
        {
            new Encounter(
                id: 1,
                name: "Goblin Den",
                minEnemies: 2,
                maxEnemies: 4,
                types: new List<EnemyType>
                {
                    Goblin.Instance
                },
                archetypes: new List<Archetype>
                {
                    Warrior.Instance,
                    Assassin.Instance,
                    Shaman.Instance
                },
                abilities: new List<Ability>
                {
                    new Fireball(),
                    new Heal(),
                    new Tackle()
                },
                presets: new List<EnemyPreset>
                {
                    new EnemyPreset
                    (
                        Goblin.Instance,
                        Berserker.Instance,
                        new Tackle(),
                        "Glorbo"
                    ),

                    new EnemyPreset
                    (
                        Goblin.Instance,
                        Shaman.Instance,
                        new Heal(),
                        "Gabi"
                    )
                },
                useOnlyPresets: false
                ),

            new Encounter(
                id: 2,
                name: "Enchanted Graveyard",
                minEnemies: 3,
                maxEnemies: 6,
                types: new List<EnemyType>
                {
                    Skeleton.Instance
                },
                archetypes: new List<Archetype>
                {
                    Warrior.Instance,
                    Berserker.Instance,
                    Assassin.Instance
                },
                abilities: new List<Ability>
                {
                    new Tackle()
                },
                presets: new List<EnemyPreset>
                {
                    new EnemyPreset
                    (
                        Skeleton.Instance,
                        Shaman.Instance,
                        new Heal(),
                        "Fallen Wizard"
                    ),

                    new EnemyPreset
                    (
                        Skeleton.Instance,
                        Berserker.Instance,
                        new Fireball(),
                        "Possessed Skeleton"
                    )
                },
                useOnlyPresets: false
                ),

            new Encounter(
                id: 3,
                name: "Sudden Ambush",
                minEnemies: 1,
                maxEnemies: 4,
                types: new List<EnemyType>
                {
                    Beastman.Instance
                },
                archetypes: new List<Archetype>
                {
                    Warrior.Instance,
                    Berserker.Instance,
                    Shaman.Instance,
                    Assassin.Instance
                },
                abilities: new List<Ability>
                {
                    new Tackle(),
                    new Fireball(),
                    new Heal()
                },
                presets: new List<EnemyPreset>
                {
                    new EnemyPreset
                    (
                        Beastman.Instance,
                        Warrior.Instance,
                        new Heal(),
                        "Muscular Beastman"
                    ),

                    new EnemyPreset
                    (
                        Beastman.Instance,
                        Shaman.Instance,
                        new Tackle(),
                        "Fred"
                    ),

                    new EnemyPreset
                    (
                        Goblin.Instance,
                        Assassin.Instance,
                        new Tackle(),
                        "Confused Goblin"
                    )
                },
                useOnlyPresets: false
                )
        };

        public static IReadOnlyList<Encounter> Boss { get; } = new List<Encounter>
        {
            new Encounter(
                id: 1,
                name: "The Final Encounter",
                minEnemies: 1,
                maxEnemies: 1,
                types: new List<EnemyType>
                {
                    Chimera.Instance
                },
                archetypes: new List<Archetype>
                {
                    Warrior.Instance,
                    Assassin.Instance,
                    Shaman.Instance
                },
                abilities: new List<Ability>
                {
                    new Fireball(),
                    new Tackle()
                },
                presets: new List<EnemyPreset>
                {
                    new EnemyPreset
                    (
                        Chimera.Instance,
                        Berserker.Instance,
                        new Fireball(),
                        "Angry Chimera"
                    ),

                    new EnemyPreset
                    (
                        Chimera.Instance,
                        Warrior.Instance,
                        new Heal(),
                        "Suffering Chimera"
                    )
                },
                useOnlyPresets: false
                )

        };
    }
}

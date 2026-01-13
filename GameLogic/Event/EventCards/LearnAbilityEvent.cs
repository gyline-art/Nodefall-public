using Nodefall.Models;
using Nodefall.Models.Abilities;
using Nodefall.Models.Archetypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Event.EventCards
{
    public abstract class LearnAbilityEvent : EventCard
    {
        public abstract Ability NewAbility { get; }

        public override void Apply(GameManager game)
        {
            game.Player.Character.Ability = NewAbility;
        }
    }

    public sealed class LearnFireball : LearnAbilityEvent
    {
        public static readonly LearnFireball Instance = new();
        public override string Title => "Fireball Spell";
        public override string Description => "A local Spellcaster offers to teach you";
        public override Ability NewAbility => new Fireball();
    }
    public sealed class LearnHeal : LearnAbilityEvent
    {
        public static readonly LearnHeal Instance = new();
        public override string Title => "Healing Spell";
        public override string Description => "A local Spellcaster offers to teach you";
        public override Ability NewAbility => new Heal();
    }
    public sealed class LearnTackle : LearnAbilityEvent
    {
        public static readonly LearnTackle Instance = new();
        public override string Title => "Passing Encounter";
        public override string Description => "A passing warrior Tackles you... you seem inspired to copy his moves";
        public override Ability NewAbility => new Tackle();
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameLogic.Event.EventCards
{
    public abstract class GainRewardsEvent : EventCard
    {
        public abstract (int gold, int exp) Rewards { get; }

        public override void Apply(GameManager game)
        {
            game.Player.Exp += Rewards.exp;
            if (game.Player.Exp < 0) game.Player.Exp = 0;

            game.Player.Gold += Rewards.gold;
            if (game.Player.Gold < 0) game.Player.Gold = 0;
        }
    }

    public sealed class MysteriousShrine : GainRewardsEvent
    {
        public static readonly MysteriousShrine Instance = new();
        public override string Title => "Mysterious Shrine";
        public override string Description => "You visit a shrine on a lonely hill...";
        public override (int gold, int exp) Rewards => (3,6);
    }
    public sealed class ArenaTournament : GainRewardsEvent
    {
        public static readonly ArenaTournament Instance = new();
        public override string Title => "Arena Tournament";
        public override string Description => "You participate in a local turnament. Maybe you'll gain some rewards?";
        public override (int gold, int exp) Rewards => (4,5);
    }
    public sealed class Folktale : GainRewardsEvent
    {
        public static readonly Folktale Instance = new();
        public override string Title => "Folktale";
        public override string Description => "A blind man offers you a story for a little compensation...";
        public override (int gold, int exp) Rewards => (-2,5);
    }
}

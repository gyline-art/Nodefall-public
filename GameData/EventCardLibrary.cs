using Nodefall.GameLogic.Event;
using Nodefall.GameLogic.Event.EventCards;
using Nodefall.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.GameData
{
    public static class EventCardLibrary
    {

        public static readonly List<EventCard> cards = new List<EventCard>
        {
            LearnFireball.Instance, LearnHeal.Instance, LearnTackle.Instance,
            MysteriousShrine.Instance, ArenaTournament.Instance, Folktale.Instance,

        };

        public static List<EventCard> Draw(int count)
        {
            List<EventCard> result = new();
            for (int i = 0; i < count; i++)
            {
                EventCard card = null;
                while (card == null || result.Contains(card))
                {
                    card = RandomUtil.PickOne(cards);
                }
                result.Add(card);
            }
            return result;
        }
    }
}

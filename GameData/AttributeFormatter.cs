using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Nodefall.GameData
{
    public static class AttributeFormatter
    {
        public static string FormatCharacter(Character c)
        {
            string name = c.Name != null ? c.Name : string.Empty;
            string archetype = c.Archetype != null ? $"({c.Archetype.Name})" : string.Empty;
            string ability = c.Ability != null ? c.Ability.Name : "None";


            int pad = 15;
            var sb = new StringBuilder();

            sb.AppendLine($"{Pad(name.ToUpper(), pad)} {archetype}");
            sb.AppendLine($"{Pad("HP"       , pad)}: {c.MaxHP}");
            sb.AppendLine($"{Pad("ATK"      , pad)}: {c.ATK}");
            sb.AppendLine($"{Pad("INT"      , pad)}: {c.MaxMP}");
            sb.AppendLine($"{Pad("Ability"  , pad)}: {ability}");

          // if (c.Archetype.Advantage != null)
          //     sb.AppendLine($"{Pad("Advantage", pad)}: {c.Archetype.Advantage}");

            return sb.ToString();
        }

        //public static string FormatArchetype(Archetype a)
        //{
        //}

        //public static string FromatAbility(Ability a)
        //{
        //}

        private static string Pad(string text, int width)
        {
            return text.PadLeft(width);
        }
    }
}

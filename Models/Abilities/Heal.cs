using Nodefall.GameLogic.Combat.Advantage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models.Abilities
{
    public class Heal : Ability
    {
        public override string Name => "Heal";
        public override string Description => "Heal yourself for 25% of your max HP";
        public override AbilityTargetType Type => AbilityTargetType.Allies;
        public override void Use(Character user, IEnumerable<Character> targets)
        {
            foreach (var target in targets)
            {
                var healAmount = (int)Math.Ceiling((float)target.CurrentHP / 4);
                target.CurrentHP += healAmount;
                if (target.CurrentHP > target.MaxHP) 
                    target.CurrentHP = target.MaxHP;
            }
            user.IsDefending = false;
            user.CurrentMP--;
        }
        public override Ability CreateCopy() => new Heal();
    }
}

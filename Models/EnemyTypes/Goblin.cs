using Nodefall.GameLogic.Combat.Advantage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models.EnemyTypes
{
    public sealed class Goblin : EnemyType
    {
        public static readonly Goblin Instance = new();
        public override string Name => "Goblin";
        public override int BaseHP => 5;
        public override int BaseMP => 3;
        public override int BaseATK => 3;
        public override float DifficultyScaling => 1.3f;

        public override List<Advantage> Advantages => new();

        public override int Gold => 2;
        public override int Exp => 4;
    }
}

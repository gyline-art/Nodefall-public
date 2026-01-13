using Nodefall.GameLogic.Combat.Advantage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models.EnemyTypes
{
    public sealed class Chimera : EnemyType
    {
        public static readonly Chimera Instance = new();
        public override string Name => "Chimera";
        public override int BaseHP => 15;
        public override int BaseMP => 6;
        public override int BaseATK => 4;
        public override float DifficultyScaling => 1.4f;

        public override List<Advantage> Advantages => new();

        public override int Gold => 10;
        public override int Exp => 15;
    }
}

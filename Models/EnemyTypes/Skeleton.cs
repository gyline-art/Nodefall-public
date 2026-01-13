using Nodefall.GameLogic.Combat.Advantage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Models.EnemyTypes
{
    public sealed class Skeleton : EnemyType
    {
        public static readonly Skeleton Instance = new();
        public override string Name => "Skeleton";
        public override int BaseHP => 2;
        public override int BaseMP => 1;
        public override int BaseATK => 5;
        public override float DifficultyScaling => 1.3f;

        public override List<Advantage> Advantages => new();

        public override int Gold => 4;
        public override int Exp => 2;
    }
}

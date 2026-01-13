using Nodefall.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Interfaces
{
    public interface IScenario
    {
        string Name { get; }
        void Execute(GameManager game);
    }
}

using Nodefall.GameLogic.Combat.Advantage;
using Nodefall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodefall.Interfaces
{
    public interface IAdvantage
    {
        List<Advantage> Advantages { get; }
    }
}

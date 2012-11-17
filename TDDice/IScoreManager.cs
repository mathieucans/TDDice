using System.Collections.Generic;

namespace TDDice
{
    public interface IScoreManager
    {
        int Calculate(IEnumerable<IDice> dices);
    }
}

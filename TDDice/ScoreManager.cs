using System.Collections.Generic;
using System.Linq;

namespace TDDice
{
    public class ScoreManager : IScoreManager
    {
        public int Calculate(IEnumerable<IDice> dices)
        {
            return dices.Sum(d => d.Value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TDDice
{
    public class MoreThan30LessThan12Game
    {
        private IList<object> _savedPanel;

        public IEnumerable<object> PlayPanel { get; private set; }

        public IEnumerable<object> SavedPanel { get { return _savedPanel;} }

        public MoreThan30LessThan12Game()
        {
            PlayPanel = new object[6];
            _savedPanel = new object[0];
        }

        public void SaveDice(object dice)
        {
            _savedPanel.Add(dice);
        }
    }
}

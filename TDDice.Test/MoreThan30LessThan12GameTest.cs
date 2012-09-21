using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TDDice.Test
{
    [TestFixture]
    public class MoreThan30LessThan12GameTest
    {
        [TestCase]
        public void PlayZone_AtInit_ShouldContains6Elements()
        {
            var game = new MoreThan30LessThan12Game();

            Assert.AreEqual(6, game.PlayPanel.Count());
        }

        [TestCase]
        public void SavedZone_AtInit_ShouldContains0Elements()
        {
            var game = new MoreThan30LessThan12Game();

            Assert.AreEqual(0, game.SavedPanel.Count());
        }

        [TestCase]
        public void SaveDice_WithFirstPlayedDice_ShoudAddDiceToTheSavedPanel()
        {
            var game = new MoreThan30LessThan12Game();
            var dice = game.PlayPanel.First();

            game.SaveDice(dice);

            CollectionAssert.Contains(game.SavedPanel, dice);
        }

    }
}

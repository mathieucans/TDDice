using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollectionAssert = MSTestExtensions.CollectionAssert;

namespace TDDice
{
    [TestClass]
    public class GameTest
    {
        private IDice[] dices;
        private IDiceFactory diceFactory;
        private IScoreManager scoreManager;

        [TestInitialize]
        public void Init()
        {
            dices = A.CollectionOfFake<IDice>(6).ToArray();

            diceFactory = A.Fake<IDiceFactory>();
            A.CallTo(diceFactory).WithReturnType<IDice>().ReturnsNextFromSequence(dices);

            scoreManager = A.Fake<IScoreManager>();
        }

        private Game CreateGame()
        {
            var game = new Game(diceFactory, scoreManager);
            return game;
        }

        #region Baby Steps

        [TestMethod]
        public void TheGameShouldHave6RollableDices()
        {
            var game = CreateGame();

            CollectionAssert.LengthEqual(game.RollableDices, 6);
        }

        [TestMethod]
        public void TheGameShouldHaveAnEmptyFrozenDicesCollection()
        {
            var game = CreateGame();

            CollectionAssert.LengthEqual(game.FrozenDices, 0);
        }

        [TestMethod]
        public void FreezeFirstRollableDiceShouldRemoveItFromTheRollableDicesCollection()
        {
            var game = CreateGame();
            var dice = game.RollableDices.First();
            game.Play();

            game.Freeze(dice);

            CollectionAssert.DoesNotContain(game.RollableDices, dice);
        }

        [TestMethod]
        public void FreezeFirstRollableDiceShouldAddItToTheFrozenDicesCollection()
        {
            var game = CreateGame();
            var dice = game.RollableDices.First();
            game.Play();

            game.Freeze(dice);

            CollectionAssert.Contains(game.FrozenDices, dice);
        }

        #endregion

        #region Fake / Injection

        [TestMethod]
        public void PlayShouldRollEachRollableDice()
        {
            var game = CreateGame();

            game.Play();

            foreach (var dice in dices)
            {
                A.CallTo(() => dice.Roll()).MustHaveHappened(Repeated.Exactly.Once);
            }
        }

        #endregion

        #region Requirements change

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FreezeOneDiceWithoutPlayingShouldThrowAnException()
        {
            var game = CreateGame();

            game.Freeze(game.RollableDices.First());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FreezeASecondDiceWithoutPlayingShouldThrowAnException()
        {
            var game = CreateGame();
            game.Play();
            game.Freeze(game.RollableDices.First());

            game.Freeze(game.RollableDices.First());
        }

        #endregion

        #region Single responsability

        //[TestMethod]
        //public void ScoreAtInitShouldBe0()
        //{
        //    var game = CreateGame();

        //    Assert.AreEqual(0, game.Score);
        //}

        //[TestMethod]
        //public void ScoreWhenFreezingADiceWithValue5ShouldBe5()
        //{
        //    var game = CreateGame();

        //    game.Play();
        //    var dice = game.RollableDices.First();
        //    A.CallTo(() => dice.Value).Returns(5);
        //    game.Freeze(dice);

        //    Assert.AreEqual(5, game.Score);
        //}

        //[TestMethod]
        //public void ScoreWhenFreezingTwoDicesWithValue5and6ShouldBe11()
        //{
        //    var game = CreateGame();

        //    game.Play();
        //    var dice = game.RollableDices.First();
        //    A.CallTo(() => dice.Value).Returns(5);
        //    game.Freeze(dice);

        //    game.Play();
        //    dice = game.RollableDices.First();
        //    A.CallTo(() => dice.Value).Returns(6);
        //    game.Freeze(dice);

        //    Assert.AreEqual(11, game.Score);
        //}

        [TestMethod]
        public void CtorShouldSetScoreTo5WhenScoreManagerCalculateReturns5()
        {
            A.CallTo(() => scoreManager.Calculate(A<IEnumerable<IDice>>.Ignored)).Returns(5);
            var game = CreateGame();

            Assert.AreEqual(5, game.Score);
        }

        [TestMethod]
        public void FreezeShouldSetScoreTo10WhenScoreManagerCalculateReturns10()
        {
            var game = CreateGame();
            game.Play();
            A.CallTo(() => scoreManager.Calculate(game.FrozenDices)).Returns(10);

            game.Freeze(game.RollableDices.First());

            Assert.AreEqual(10, game.Score);
        }

        #endregion
    }
}

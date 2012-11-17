using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TDDice
{
    [TestClass]
    public class ScoreManagerTest
    {
        [TestMethod]
        public void CalcWithNoDiceShouldReturn0()
        {
            var scoreManager = new ScoreManager();

            var result = scoreManager.Calculate(A.CollectionOfFake<IDice>(0));

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalcWithOneDiceWithValue5ShouldEquals5()
        {
            var scoreManager = new ScoreManager();
            var dices = A.CollectionOfFake<IDice>(1);
            A.CallTo(() => dices[0].Value).Returns(5);

            var result = scoreManager.Calculate(dices);

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void CalcWithTwoDices5And6ShouldEquals11()
        {
            var scoreManager = new ScoreManager();
            var dices = A.CollectionOfFake<IDice>(2);
            A.CallTo(() => dices[0].Value).Returns(5);
            A.CallTo(() => dices[1].Value).Returns(6);

            var result = scoreManager.Calculate(dices);

            Assert.AreEqual(11, result);
        }
    }
}

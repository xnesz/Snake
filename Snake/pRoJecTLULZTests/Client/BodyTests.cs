using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using pRoJecTLULZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pRoJecTLULZ.Tests
{
    [TestClass()]
    public class BodyTests
    {
        Mock<Apple> apple;
        Mock<Board> board;
        Body body;

        [TestInitialize]
        public void TestInitialize()
        {
            apple = new Mock<Apple>();
            board = new Mock<Board>();
            body = new Body(board.Object);
        }

        [TestMethod()]
        public void GrowTailMatchPos()
        {
            Assert.AreEqual(0, body.score);

            Dir pos = new Dir(body.x, body.y);
            body.GrowTail(pos, apple.Object);

            Assert.AreEqual(1, body.score);
        }

        [TestMethod()]
        public void GrowTailNoMatchPos()
        {
            Assert.AreEqual(0, body.score);

            Dir pos = new Dir(body.x + 1, body.y);
            body.GrowTail(pos, apple.Object);

            Assert.AreEqual(0, body.score);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void GrowTailNullPointer()
        {
            body.GrowTail(null, apple.Object);
        }

    }
}
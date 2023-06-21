using NUnit.Framework;
using pRoJecTLULZ;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace pRoJecTLULZ.Tests
{
    [TestClass()]
    public class AppleTests
    {
        private Apple _apple;
        private Board _board;

        [TestInitialize]
        public void SetUp()
        {
            _board = new Board();
            _apple = new Apple();
        }

        [TestMethod()]
        public void ApplePosCorrectPosition()
        {
            var applePos = _apple.ApplePos();

            Assert.IsTrue(applePos.x >= 2 && applePos.x < _board.Width);
            Assert.IsTrue(applePos.y >= 2 && applePos.y < _board.Height);
        }

       /* [TestMethod()]
        public void NewApplePosChangePosition()
        {
            var oldPos = _apple.ApplePos();
            _apple.NewApplePos();
            var newPos = _apple.ApplePos();

            Assert.IsFalse(oldPos.x == newPos.x && oldPos.y == newPos.y);
        }*/
    }
}
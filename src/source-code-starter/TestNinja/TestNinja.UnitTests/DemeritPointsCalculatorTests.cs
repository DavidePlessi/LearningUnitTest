using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _dpc;

        [SetUp]
        public void SetUp()
        {
            _dpc = new DemeritPointsCalculator();
        }
        
        [Test]
        [TestCase(-1)]
        [TestCase(400)]
        public void DemeritPointsCalculator_SpeedIsOutOfRange_ThrowArgumentOutOfRangeException(int speed)
        {
            Assert.That(
                () => _dpc.CalculateDemeritPoints(speed), 
                Throws.Exception.TypeOf<ArgumentOutOfRangeException>()
            );
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(15, 0)]
        [TestCase(65, 0)]
        [TestCase(70, 1)]
        [TestCase(76, 2)]
        [TestCase(81, 3)]
        public void DemeritPointsCalculator_WhenCalled_ReturnDemeritPoints(int speed, int expectedDemeritPoints)
        {
            var res = _dpc.CalculateDemeritPoints(speed);
            Assert.That(res, Is.EqualTo(expectedDemeritPoints));
        }
    }
}
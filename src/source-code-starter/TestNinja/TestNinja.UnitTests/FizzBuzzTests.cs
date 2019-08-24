using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        [TestCase(3)]
        [TestCase(12)]
        [TestCase(27)]
        public void GetOutput_NumberDivisibleOnlyBy3_ReturnFizz(int number)
        {
            var result = FizzBuzz.GetOutput(number);
            Assert.That(result, Is.EqualTo("Fizz"));
        }
        
        [Test]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(40)]
        public void GetOutput_NumberDivisibleOnlyBy5_ReturnFizz(int number)
        {
            var result = FizzBuzz.GetOutput(number);
            Assert.That(result, Is.EqualTo("Buzz"));
        }
        
        [Test]
        [TestCase(15)]
        [TestCase(30)]
        [TestCase(45)]
        public void GetOutput_NumberDivisibleBy3And5_ReturnFizzBuzz(int number)
        {
            var result = FizzBuzz.GetOutput(number);
            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }
        
        [Test]
        [TestCase(14)]
        [TestCase(29)]
        [TestCase(46)]
        public void GetOutput_NumberNotDivisibleBy3Or5_ReturnNumberToString(int number)
        {
            var result = FizzBuzz.GetOutput(number);
            Assert.That(result, Is.EqualTo(number.ToString()));
        }
    }
}
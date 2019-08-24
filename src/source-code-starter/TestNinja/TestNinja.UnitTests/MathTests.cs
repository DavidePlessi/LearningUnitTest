using System.IO;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;
        
        /* [SetUp]
         *
         * You can create a method here and decorate it with a SetUp attribute and the NUnit test runner will call that
         * method BEFORE running each test
         */ 
        [SetUp]
        public void SetUp()
        {
            _math = new Math();
            // So we write the initialization of math class one time but it will run this code before running each test
        }
        
        /* [TearDown]
         *
         * You can create a method here and decorate it with a TearDown attribute and the NUnit test runner will call
         * that method AFTER running each test
         */
        
        /* [Ignore()]
         *
         * This argument take a string that is the reason why, this is very usefull because we can see this message
         * when running our tests.
         * The test runner will ignore the test 
         */
        
        [Test]
        [Ignore("Because I wanted to!")]
        public void Add_WhenCalled_ReturTheSumOfArguments()
        {
            var result = _math.Add(1, 2);
            
            Assert.That(result, Is.EqualTo(3));
        }
        
        /* Parameterized Tests
         *
         * We can declare a generic method that take arguments and use the [TestCase()] decorator to call it multiple
         * time with different parameter
         */
        [Test]
        [TestCase(1, 2, 2)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 2)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            Assert.That(_math.Max(a, b), Is.EqualTo(expectedResult));
        }
        
        //  OLD ADD TEST        
        //  [Test]
        //  public void Max_FirstArgumentGreater_ReturnFirstArgument()
        //  {
        //      var result = _math.Max(2, 1);
        //      
        //      Assert.That(result, Is.EqualTo(2));
        //  }
        //  
        //  [Test]
        //  public void Max_SecondArgumentGreater_ReturnSecondArgument()
        //  {
        //      var result = _math.Max(2, 5);
        //      
        //      Assert.That(result, Is.EqualTo(5));
        //  }
        //  
        //  [Test]
        //  public void Max_ArgumentAreEqual_ReturnTheSameArgument()
        //  {
        //      var result = _math.Max(5, 5);
        //      
        //      Assert.That(result, Is.EqualTo(5));
        //  }
    }
}
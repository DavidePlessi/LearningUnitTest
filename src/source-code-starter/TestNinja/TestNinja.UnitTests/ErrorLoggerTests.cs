using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        private ErrorLogger _errorLogger;
        
        [SetUp]
        public void SetUp()
        {
            _errorLogger = new ErrorLogger();
        }
        
        //Testing void methods
        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            
            _errorLogger.Log("a");
            Assert.That(_errorLogger.LastError, Is.EqualTo("a"));
        }

        //Testing void methods with exceptions
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullExceptio(string error)
        {
            Assert.That(() => _errorLogger.Log(error), Throws.ArgumentNullException);
        }
        
        //Testing void methods that rise event
        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var id = Guid.Empty;
            
            _errorLogger.ErrorLogged += (sender, args) => { id = args; };
            _errorLogger.Log("a");
            
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
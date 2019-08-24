using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        private HtmlFormatter _formatter;
        [SetUp]
        public void SetUp()
        {
            _formatter = new HtmlFormatter();
        }
        
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            var res = _formatter.FormatAsBold("abc");
            
            // Check is case sensitive, to make it not sensitive just add .IgnoreCase
            
            // Specific - less maintainable
            Assert.That(res, Is.EqualTo("<strong>abc</strong>").IgnoreCase);
            
            // More general - less trustworthy
            Assert.That(res, Does.StartWith("<strong>").IgnoreCase);
            
            // Less general
            Assert.That(res, Does.StartWith("<strong>").IgnoreCase);
            Assert.That(res, Does.EndWith("</strong>").IgnoreCase);
            Assert.That(res, Does.Contain("abc").IgnoreCase);
        }
    }
}
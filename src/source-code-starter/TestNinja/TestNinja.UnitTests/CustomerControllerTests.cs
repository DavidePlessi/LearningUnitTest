using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController _customer;
        
        [SetUp]
        public void SetUp()
        {
            _customer = new CustomerController();
        }
        
        [Test]
        public void GetCustomer_IdIsZero_ReturnNorFound()
        {
            var res = _customer.GetCustomer(0);
            
            //First way -> NotFound
            Assert.That(res, Is.TypeOf<NotFound>());
            //Second way -> NorFound or one of its derivates
//            Assert.That(res, Is.InstanceOf<NotFound>());
            // The difference
            // InstanceOf means the result are NotFound type or one of it's derivatives
            // TypeOf check the exactly type
        }
        
        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            var res = _customer.GetCustomer(1);
            
            Assert.That(res, Is.TypeOf(typeof(Ok)));
            
        }
    }
}
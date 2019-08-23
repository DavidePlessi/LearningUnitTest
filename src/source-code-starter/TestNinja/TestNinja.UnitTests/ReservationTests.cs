using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]//This attributes belong to MSTestFramework
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange (where we initialize our objects)
            var reservation = new Reservation();

            // Act (where we act on this object)
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });


            // Assert (where we verify the result)
            Assert.IsTrue(result);
            //or
            Assert.That(result, Is.True);
            //or
            Assert.That(result == true);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancellingTheReservation_ReturnsTrue()
        {
            var user = new User();
            var reservation = new Reservation() { MadeBy = user };

            var result = reservation.CanBeCancelledBy(user);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanBeCancelledBy_UserCantCancelReservation_ReturnsFalse()
        {
            var reservation = new Reservation() { MadeBy = new User() };

            var result = reservation.CanBeCancelledBy(new User());

            Assert.IsFalse(result);
        }

        
    }
}

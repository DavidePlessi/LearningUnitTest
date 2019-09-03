using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperTests_OverlappingBookingExist
    {
        private Booking _existingBooking;
        private Mock<IBookingReporistory> _mockRepo;

        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };
            _mockRepo = new Mock<IBookingReporistory>();
            _mockRepo
                .Setup(r => r.GetActiveBookings(1))
                .Returns(new List<Booking>
                {
                    _existingBooking
                }.AsQueryable());
        }

        [Test]
        public void BookingStartAndFinishesBeforeExistingBooking_ReturnEmptyString()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, 2),
                DepartureDate = Before(_existingBooking.ArrivalDate)
            }, _mockRepo.Object);
            
            Assert.That(res, Is.Empty);
        }

        [Test]
        public void BookingStartBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBooking()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate)
            }, _mockRepo.Object);
            
            Assert.That(res, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBooking()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _mockRepo.Object);
            
            Assert.That(res, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBooking()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate)
            }, _mockRepo.Object);
            
            Assert.That(res, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartInTheMiddleOfAndFinishesAfterAnExistingBooking_ReturnExistingBooking()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _mockRepo.Object);
            
            Assert.That(res, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartAndFinishesAfterAnExistingBooking_ReturnExistingBooking()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, 2)
            }, _mockRepo.Object);
            
            Assert.That(res, Is.Empty);
        }
        
        [Test]
        public void BookingOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate),
                Status = "Cancelled"
            }, _mockRepo.Object);
            
            Assert.That(res, Is.Empty);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }
        
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
    }
}
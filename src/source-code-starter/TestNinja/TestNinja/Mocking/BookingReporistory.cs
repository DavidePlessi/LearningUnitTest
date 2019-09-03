using System.Linq;

namespace TestNinja.Mocking
{
    public interface IBookingReporistory
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }

    public class BookingReporistory : IBookingReporistory
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings = unitOfWork
                .Query<Booking>()
                .Where(b => (!excludedBookingId.HasValue || b.Id != excludedBookingId.Value) && b.Status != "Cancelled");

            return bookings;
        }
    }
}
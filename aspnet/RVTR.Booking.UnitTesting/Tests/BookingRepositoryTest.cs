using System;
using RVTR.Booking.DataContext;
using RVTR.Booking.DataContext.Repositories;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class BookingRepositoryTest : DataTest
  {
    [Fact]
    public async void Test_GetBookingsById()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);
      var actual = await bookings.GetByAccountId(1);

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_GetBookingsAsync_ByDate()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);
      var actual = await bookings.GetBookingsByDatesAsync(new DateTime(2020, 8, 17), new DateTime(2020, 8, 19));

      Assert.NotEmpty(actual);
    }
  }
}

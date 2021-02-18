using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.Context;
using RVTR.Booking.Context.Repositories;
using RVTR.Booking.Domain.Models;
using Xunit;

namespace RVTR.Booking.Testing.Tests
{
  public class RepositoryTest : DataTest
  {

    private readonly BookingModel _booking = new BookingModel { Id = 3, AccountId = 1, LodgingId = 1 };

    [Fact]
    public async void Test_Repository_DeleteAsync()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new Repository<BookingModel>(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          AccountId = 1,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel()
            {
              Id = 0,
              BookingModelId = 0,
              FirstName = "First Name User",
              LastName = "Last Name User",
              IsMinor = false
            }
          },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var booking = await ctx.Bookings.FirstAsync();
      await bookings.DeleteAsync(booking.Id);

      Assert.Equal(EntityState.Deleted, ctx.Entry(booking).State);
    }

    [Fact]
    public async void Test_Repository_InsertAsync()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new Repository<BookingModel>(ctx);

      await bookings.InsertAsync(_booking);

      Assert.Equal(EntityState.Added, ctx.Entry(_booking).State);
    }

    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new Repository<BookingModel>(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          AccountId = 1,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel()
            {
              Id = 0,
              BookingModelId = 0,
              FirstName = "First Name User",
              LastName = "Last Name User",
              IsMinor = false
            }
          },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var actual = await bookings.SelectAsync();

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new Repository<BookingModel>(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          AccountId = 1,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel()
            {
              Id = 0,
              BookingModelId = 0,
              FirstName = "First Name User",
              LastName = "Last Name User",
              IsMinor = false
            }
          },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var actual = await bookings.SelectAsync(1);

      Assert.NotNull(actual);
    }

    [Fact]
    public async void Test_Repository_Update()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new Repository<BookingModel>(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          AccountId = 1,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel()
            {
              Id = 0,
              BookingModelId = 0,
              FirstName = "First Name User",
              LastName = "Last Name User",
              IsMinor = false
            }
          },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var booking = await ctx.Bookings.FirstAsync();

      booking.CheckOut = DateTime.Now;
      bookings.Update(booking);

      var result = ctx.Bookings.Find(booking.Id);
      Assert.Equal(booking.CheckOut, result.CheckOut);
      Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
    }
  }
}

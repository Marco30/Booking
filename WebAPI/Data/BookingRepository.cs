using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class BookingRepository : IBookingRepository<Booking>
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Booking GetById(int id)
        {
            return _context.Bookings.Find(id);
        }

        public IList<Booking> GetByUserId(int id)
        {
            return _context.Bookings.Where(i => i.PersonId == id).ToList();
        }

        public IList<Booking> GetAllList()
        {
            return _context.Bookings.ToList();
        }

        public void Add(Booking entity)
        {
            _context.Bookings.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Booking entity)
        {
            _context.Bookings.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.Bookings.Where(i => i.Id == id).Single();

            _context.Bookings.Remove(item);
            _context.SaveChanges();
        }

        public DbSet<Booking> GetAll()
        {
            return _context.Bookings;
        }
    }
}
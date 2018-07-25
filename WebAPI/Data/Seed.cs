using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Helper;
using WebAPI.Models;

namespace WebAPI.Data
{
    public interface IDbInitializer
    {
        void Initialize(BookingDbContext _context);
    }

    public class DbInitializer : IDbInitializer
    {
        public void Initialize(BookingDbContext _context)
        {
            _context.Database.EnsureCreated();

            if (_context.Persons.Any(r => r.Name != null)) return;

            string userName = "admin";

            var hash = SecurePasswordHasher.Hash("test123");

            _context.Persons.Add(new Person() { Name = userName, Password = hash, Email = "Marco.Villegas@live.se" });

            _context.SaveChanges();

            Person person = _context.Persons.FirstOrDefault(u => u.Name == userName);

            DateTime date = DateTime.Parse("2018-07-11");
            _context.Bookings.Add(new Booking() { Person = person, Date = date, Time = "11.00-14.00" });

            _context.SaveChanges();

            userName = "user1";

            _context.Persons.Add(new Person() { Name = userName, Password = hash, Email = "user1@live.se" });

            _context.SaveChanges();

            person = _context.Persons.FirstOrDefault(u => u.Name == userName);

            date = DateTime.Parse("2018-07-12");
            _context.Bookings.Add(new Booking() { Person = person, Date = date, Time = "11.00-14.00" });

            _context.SaveChanges();

            userName = "user2";

            _context.Persons.Add(new Person() { Name = userName, Password = hash, Email = "user2@live.se" });

            _context.SaveChanges();

            person = _context.Persons.FirstOrDefault(u => u.Name == userName);

            date = DateTime.Parse("2018-07-13");
            _context.Bookings.Add(new Booking() { Person = person, Date = date, Time = "11.00-14.00" });

            _context.SaveChanges();

            // MoneyAccount money = _context.MoneyAccounts.FirstOrDefault(u => u.Id == card.Id);

            // _context.Balances.Add(new Balance() { Id = money.Id, Ammount = 88.99, Currency = "kr" });// pungetn i ammount kan behöva var ,
            //_context.SaveChanges();
        }
    }
}
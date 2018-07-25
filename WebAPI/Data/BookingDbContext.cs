using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class BookingDbContext : DbContext
    {
        /*public BookingDbContext() : base()
        {
        }*/

        public BookingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Person> Persons { get; set; }
    }
}
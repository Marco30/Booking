using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly BookingDbContext _context;

        public PersonRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Person GetById(int id)
        {
            return _context.Persons.Find(id);
        }

        public IList<Person> GetAllList()
        {
            return _context.Persons.ToList();
        }

        public void Add(Person entity)
        {
            _context.Persons.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Person entity)
        {
            _context.Persons.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Person entity)
        {
            _context.Persons.Remove(entity);
            _context.SaveChanges();
        }

        public DbSet<Person> GetAll()
        {
            return _context.Persons;
        }
    }
}
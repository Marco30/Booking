using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data
{
    public interface IBookingRepository<T> where T : class
    {
        T GetById(int id);

        IList<T> GetByUserId(int id);

        DbSet<T> GetAll();

        IList<T> GetAllList();

        void Add(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
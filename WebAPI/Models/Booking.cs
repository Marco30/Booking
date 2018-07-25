using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
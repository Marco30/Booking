using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using WebAPI.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingRepository<Booking> _IBookingRepository;

        public BookingController(IBookingRepository<Booking> repository)
        {
            _IBookingRepository = repository;
        }

        // GET: api/<controller>
        /*[HttpGet]
        public IEnumerable<string> GetText()
        {
            var person = _IRepository.GetAllList();

            string name = "";

            string email = "";

            foreach (var item in _IRepository.GetAllList())
            {
                name = item.Name;
                email = item.Email;
            }

            return new string[] { name, email };
        }*/

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<Booking> Get()
        {
            //return new string[] { "value1", "value2" };
            return _IBookingRepository.GetAllList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Booking Get(int id)
        {
            return _IBookingRepository.GetById(id);
        }

        // GET api/<controller>/user?id=1
        [HttpGet("user")]
        [EnableCors("MyPolicy")]
        public IEnumerable<Booking> GetByUserId([FromQuery]int Id)
        {
            return _IBookingRepository.GetByUserId(Id);
        }

        // GET api/<controller>/month?id=1&month=07
        [HttpGet("month")]
        [EnableCors("MyPolicy")]
        public IEnumerable<Booking> GetByUserIdAndMonth([FromQuery]int Id, [FromQuery]string Month)
        {
            var list = _IBookingRepository.GetByUserId(Id);

            list = list.Where(i => i.PersonId == Id && i.Date.ToString("yyyy-MM") == Month).ToList();

            return list;
        }

        // POST api/<controller>
        [HttpPost]
        [EnableCors("MyPolicy")]
        public ResponseMessage Post([FromBody] Booking Booking)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            var exceededBookings = false;
            var addData = false;

            Booking.Time = RemoveAllWhiteSpace.RemoveInPlaceCharArray(Booking.Time);

            //var list = _IBookingRepository.GetByUserId(Booking.PersonId);

            var list = _IBookingRepository.GetAllList();

            var userlist = _IBookingRepository.GetByUserId(Booking.PersonId);
            userlist = userlist.Where(i => i.Date.ToString("YYYY-MM") == Booking.Date.ToString("YYYY-MM")).ToList();

            if (userlist.Count >= 3)
            {
                exceededBookings = false;

                responseMessage.Status = exceededBookings;
                responseMessage.Message = "you have exceeded monthly bookings";

                return responseMessage;
            }

            list = list.Where(i => i.Date.Date == Booking.Date.Date && i.Time == Booking.Time).ToList();

            bool OtherUser = false;

            foreach (var item in list)
            {
                if (item.PersonId != Booking.PersonId)
                {
                    OtherUser = true;
                    break;
                }
            }

            bool isEmpty = !list.Any();
            if (isEmpty)
            {
                _IBookingRepository.Add(Booking);
                addData = true;

                responseMessage.Status = addData;
                responseMessage.Message = "new booking has been added";

                return responseMessage;
            }
            else
            {
                addData = false;// booking date already exist
                responseMessage.Status = addData;

                responseMessage.Message = "date and time is already booked";

                if (OtherUser == true)
                {
                    responseMessage.Message = "date and time is already booked by other user";
                }

                return responseMessage;
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [EnableCors("MyPolicy")]
        public void Delete(int id)
        {
            _IBookingRepository.Delete(id);
        }
    }
}
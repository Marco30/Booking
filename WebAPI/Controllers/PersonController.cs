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
    public class PersonController : Controller
    {
        private readonly IRepository<Person> _IRepository;

        public PersonController(IRepository<Person> repository)
        {
            _IRepository = repository;
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

        // POST api/<controller>
        [HttpPost]
        [EnableCors("MyPolicy")]
        public ResponseMessage Post([FromBody] Person person)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            var list = _IRepository.GetAllList();

            list = list.Where(i => i.Name == person.Name).ToList();

            bool isEmpty = !list.Any();
            if (isEmpty)
            {
                responseMessage.Status = false;
                responseMessage.Message = "user das not exist";

                return responseMessage;
            }

            var result = SecurePasswordHasher.Verify(person.Password, list[0].Password);

            if (result)
            {
                responseMessage.Status = result;
                responseMessage.Message = "";
                responseMessage.Id = list[0].Id;

                return responseMessage;
            }
            else
            {
                responseMessage.Status = result;
                responseMessage.Message = "wrong password or username";

                return responseMessage;
            }
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            //return new string[] { "value1", "value2" };
            return _IRepository.GetAllList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return _IRepository.GetById(id);
        }

        // POST api/<controller>
        /*[HttpPost]
        public void Post([FromBody]string value)
        {
        }*/

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
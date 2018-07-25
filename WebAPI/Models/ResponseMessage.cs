using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ResponseMessage
    {
        public bool Status { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
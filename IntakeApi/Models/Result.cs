using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntakeApi.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public List<Exception> Exceptions { get; set; }
    }
}

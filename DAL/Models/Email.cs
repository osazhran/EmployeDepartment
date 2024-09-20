using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Email:ModuleBase
    {
        public string Subject { get; set; }
        public string Recipents { get; set; }
        public string Body { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consume_API_ASP_NET_CORE.Models
{
    public class modelChangePassword
    {
        public string password { get; set; }
        public string newpassword { get; set; }
        public string confirmpassword { get; set; }
    }
}

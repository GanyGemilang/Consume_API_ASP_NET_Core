using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consume_API_ASP_NET_CORE.Models
{
    public class Data
    {
        public string Token { get; set; }
    }

    public class ResponseModel
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
    }

    public class DataGet
    {
        public string username { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string email { get; set; }
    }

    public class ResponseModelGet
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<DataGet> Data { get; set; }
    }

    public class DataGetByUsername
    {
        public string username { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string token { get; set; }
        public DateTime expiredToken { get; set; }
        public bool online { get; set; }
    }

    public class ResponseModelGetByUsername
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public DataGetByUsername Data { get; set; }
    }

    public class ResponseModelChangePassword
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public int Data { get; set; }
    }
    
    public class ResponseModelDelete
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
}

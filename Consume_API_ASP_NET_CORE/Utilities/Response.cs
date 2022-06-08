using Consume_API_ASP_NET_CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consume_API_ASP_NET_CORE.Utilities
{
    public class Response
    {
        public static ResponseModel ResponseMessage(string Code, string Status, string Message, Data Data)
        {
            ResponseModel Hasil = new ResponseModel();
            Hasil.Code = Code;
            Hasil.Status = Status;
            Hasil.Message = Message;
            Hasil.Data = Data;
            return Hasil;
        }
    }
}

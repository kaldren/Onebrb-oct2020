using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.MVC.Models
{
    public class BaseApiResponse<TBody>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TBody Body { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Api.Models
{
    public abstract class BaseModel
    {
        public int ItemId { get; set; }
        public string UserId { get; set; }
        public string SecurityHash { get; set; }
    }
}

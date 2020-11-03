using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Core.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CreatorId { get; set; }
    }
}

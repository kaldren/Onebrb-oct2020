using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Core.RequestModels
{
    public class UserRequestModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}

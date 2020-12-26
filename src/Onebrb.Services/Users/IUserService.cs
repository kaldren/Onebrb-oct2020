using Onebrb.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Services.Users
{
    public interface IUserService
    {
        public Task<User> GetUser(Func<User, bool> predicate);
    }
}

using Onebrb.Core.Models;
using Onebrb.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IOnebrbContext _onebrbContext;

        public UserService(IOnebrbContext onebrbContext)
        {
            _onebrbContext = onebrbContext;
        }

        public Task<User> GetUser(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}

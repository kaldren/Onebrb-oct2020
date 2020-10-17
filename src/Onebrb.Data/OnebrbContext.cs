using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Data
{
    public class OnebrbContext : DbContext
    {
        public OnebrbContext(DbContextOptions<OnebrbContext> options) 
            : base(options)
        {
        }
    }
}

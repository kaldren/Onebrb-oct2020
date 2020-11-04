using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Onebrb.Core.Models;
using Onebrb.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Data
{
    public class OnebrbContext : IdentityDbContext<User>, IOnebrbContext
    {
        public OnebrbContext(DbContextOptions<OnebrbContext> options) 
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }

        public async Task<Item> GetItemAsync(long itemId)
        {
            return await this.Items.SingleOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<ICollection<Item>> GetItemsAsync(string userId)
        {
            User user =  await this.Users.SingleOrDefaultAsync(x => x.Id == userId);
            
            if (user is null)
            {
                return null;
            }

            return await this.Items.Where(x => x.UserId == userId).ToListAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
        }
    }
}
